using System;
using UnityEngine;    // For Debug.Log, etc.

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Runtime.Serialization;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;
using System.Collections;
using Newtonsoft.Json.Linq;

public class Hibernator : MonoBehaviour
{

	public GameObject saveing;

	void Start()
	{
	}

	void Update()
	{

	}


	public static string currentFilePath = "QuickSaveGame.les";    // Edit this for different save files

	public IEnumerable<string> LoadSaveGames()
	{
		return Directory.GetFiles(Directory.GetCurrentDirectory(), "*.les");
	}


	public void QuickSave()
	{
		Save(currentFilePath);
	}

	public void Save(string filePath)
	{
		saveing.SetActive(true);
		DoSave(filePath);
		saveing.SetActive(false);
	}

	private void DoSave(string filePath)
	{
		savedObjects = new Dictionary<ISaveable, GameObjectData>();
		var objectsToSave = UnityEngine.Object.FindObjectsOfType<GameObject>();

		SaveGame gameToStore = new SaveGame();
		var saveDatas = objectsToSave.Select(item => CreatePrefab(item)).Where(item => item != null);

		gameToStore.Prefabs.AddRange(saveDatas);
		// serialize JSON directly to a file
		using (StreamWriter file = File.CreateText(filePath))
		{

			JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
			serializer.Serialize(file, gameToStore);
		}

		currentFilePath = filePath;

	}

	private JsonSerializerSettings SerializerSettings
	{
		get
		{
			var settings = new JsonSerializerSettings();
			settings.Formatting = Formatting.Indented;
			settings.DefaultValueHandling = DefaultValueHandling.Ignore;
			return settings;
		}
	}

	private Dictionary<ISaveable, GameObjectData> savedObjects = new Dictionary<ISaveable, GameObjectData>();

	private Dictionary<int, object> loadedObjects = new Dictionary<int, object>();

	private PrefabData CreatePrefab(GameObject gameObject)
	{
		IncludeInSaveGame restore = gameObject.GetComponent<IncludeInSaveGame>();
		if (restore == null)
			return null;

		var prefabSave = new PrefabData();
		prefabSave.TemplateName = restore.PrefabName;
		prefabSave.Name = restore.name;

		if (restore.PersistPosition)
		{
			prefabSave.Position = new PersistVector3(gameObject.transform.position);
			prefabSave.Rotation = new PersistQuaternion(gameObject.transform.rotation);
			prefabSave.Scale = new PersistVector3(gameObject.transform.localScale);
		}

		ISaveable[] toBeSavedHere = gameObject.GetComponents<ISaveable>().Distinct().ToArray();

		var saveData = toBeSavedHere.Select(item => CreateSaveData(item)).ToList();
		prefabSave.GameObjects.AddRange(saveData);
		return prefabSave;
	}

	private GameObjectData CreateSaveData(ISaveable savable)
	{
		GameObjectData data;
		if (savedObjects.TryGetValue(savable, out data))
		{
			return data;
		}

		data = new GameObjectData(true)
		{
			TypeName = savable.GetType().FullName,
		};

		savedObjects.Add(savable, data);

		FillSaveData(savable, data);

		return data;
	}

	private void FillSaveData(ISaveable saveable, GameObjectData saveData)
	{
		var propertiesToSave = saveable.GetType().GetProperties().Where(property => property.GetCustomAttributes(false).OfType<SaveGameValueAttribute>().Any()).ToList();

		foreach (PropertyInfo info in propertiesToSave)
		{
			if (!info.CanRead)
			{
				continue;
			}

			var value = info.GetValue(saveable, null);
			PersistValue(saveData, info.Name, value);

		}

		var fieldsToSave = saveable.GetType().GetFields().Where(property => property.GetCustomAttributes(false).OfType<SaveGameValueAttribute>().Any()).ToList();

		foreach (FieldInfo info in fieldsToSave)
		{
			var value = info.GetValue(saveable);
			PersistValue(saveData, info.Name, value);

		}

	}

	private void PersistValue(GameObjectData saveData, string name, object value)
	{

		ISaveable saveableProperty = value as ISaveable;

		if (saveableProperty != null)
		{
			GameObjectData item = CreateSaveData(saveableProperty);
			saveData.Links.Add(name, new List<int> { item.Id });
			return;
		}

		IEnumerable<ISaveable> savableList = value as IEnumerable<ISaveable>;
		if (savableList != null)
		{
			var idList = new List<int>();
			foreach (ISaveable savablePart in savableList)
			{
				GameObjectData item = CreateSaveData(saveableProperty);
				idList.Add(item.Id);
			}

			saveData.Links.Add(name, idList);
			return;
		}

		if (value is string || value is int || value is bool)
		{
			saveData.Attributes.Add(name, value);
			return;
		}

		if (value is float[,])
		{
			saveData.Floats.Add(name, (float[,])value);
			return;
		}

	}

	public void Load(string filePath)
	{
		loadedObjects = new Dictionary<int, object>();

		SaveGame saveGame;
		using (StreamReader file = File.OpenText(filePath))
		{
			JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
			saveGame = (SaveGame)serializer.Deserialize(file, typeof(SaveGame));
		}


		Dictionary<Component, Dictionary<string, List<int>>> missingLinks = new Dictionary<Component, Dictionary<string, List<int>>>();

		foreach (PrefabData hPrefab in saveGame.Prefabs)
		{
			var prefab = Resources.Load("Prefabs/" + hPrefab.TemplateName);
			if (prefab == null)
				continue;

			Vector3 pos = Vector3.zero;
			Vector3 scale = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			if (hPrefab.Position != null)
			{
				pos = hPrefab.Position.ToVector3();
				rotation = hPrefab.Rotation.ToQuaternion();
				scale = hPrefab.Scale.ToVector3();
			}

			GameObject fromPrefab = (GameObject)GameObject.Instantiate(prefab, pos, rotation);
			fromPrefab.transform.localScale = scale;
			fromPrefab.name = hPrefab.Name;

			foreach (var go in hPrefab.GameObjects)
			{
				var component = fromPrefab.GetComponent(Type.GetType(go.TypeName));
				LoadAttributes<object>(component, go.Attributes);
				LoadAttributes<float[,]>(component, go.Floats);

				loadedObjects.Add(go.Id, component);
				missingLinks.Add(component, go.Links);
			}

		}
		foreach (var compRef in missingLinks)
		{
			Component comp = compRef.Key;
			Dictionary<string, List<int>> linksToSet = compRef.Value;
			foreach (var link in linksToSet)
			{
				string linkName = link.Key;
				List<int> links = link.Value;
				SetLink(comp, linkName, links);
			}
		}

	}

	private void SetLink(Component comp, string linkName, List<int> links)
	{
		var propertiesToSave = comp.GetType().GetProperties().Where(property => property.Name == linkName&&property.GetCustomAttributes(false).OfType<SaveGameValueAttribute>().Any()).ToList();

		foreach (PropertyInfo info in propertiesToSave)
		{
			if (typeof(IList).IsAssignableFrom(info.PropertyType))
			{
				IList list = info.GetValue(comp, null) as IList;

				foreach (var loadedObj in links.Select(id => loadedObjects[id]))
				{
					list.Add(loadedObj);
				}
			}
			else {
				var val = links.Select(id => loadedObjects[id]).FirstOrDefault();
				info.SetValue(comp, val, null);
			}

		}

		var fieldsToSave = comp.GetType().GetFields().Where(property =>property.Name==linkName && property.GetCustomAttributes(false).OfType<SaveGameValueAttribute>().Any()).ToList();

		foreach (FieldInfo info in fieldsToSave)
		{
			if (typeof(IList).IsAssignableFrom(info.FieldType))
			{
				IList list = info.GetValue(comp) as IList;

				foreach (var loadedObj in links.Select(id => loadedObjects[id]))
				{
					list.Add(loadedObj);
				}
			}
			else {
				var val = links.Select(id => loadedObjects[id]).FirstOrDefault();
				info.SetValue(comp, val);
			}

		}
	}


	private void LoadAttributes<T>(Component comp, Dictionary<string, T> attributes)
	{
		var propertiesToSave = comp.GetType().GetProperties().Where(property => property.GetCustomAttributes(false).OfType<SaveGameValueAttribute>().Any()).ToList();

		foreach (PropertyInfo info in propertiesToSave)
		{
			if (!info.CanWrite)
			{
				continue;
			}
			T val;
			if (attributes.TryGetValue(info.Name, out val))
			{
				try
				{
					info.SetValue(comp, val, null);
				}
				catch
				{
					Debug.LogError("Can not load :" + info.Name + " of type " + val.GetType().FullName);
				}
			}


		}

		var fieldsToSave = comp.GetType().GetFields().Where(property => property.GetCustomAttributes(false).OfType<SaveGameValueAttribute>().Any()).ToList();

		foreach (FieldInfo info in fieldsToSave)
		{
			T val;
			if (attributes.TryGetValue(info.Name, out val))
			{
				try
				{
					info.SetValue(comp, val);
				}
				catch
				{
					Debug.LogError("Can not load :" + info.Name + " of type " + val.GetType().FullName);
				}
			}

		}
	}


}
