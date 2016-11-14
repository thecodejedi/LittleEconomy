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
		return Directory.GetFiles(Directory.GetCurrentDirectory(),"*.les");
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
		var objectsToSave = UnityEngine.Object.FindObjectsOfType<GameObject>();

		SaveGame gameToStore = new SaveGame();
		var saveDatas = objectsToSave.Select(item => CreatePrefab(item)).Where(item => item != null);

		gameToStore.Prefabs.AddRange(saveDatas);
		// serialize JSON directly to a file
		using (StreamWriter file = File.CreateText(filePath))
		{
			var settings = new JsonSerializerSettings();
			settings.Formatting = Formatting.Indented;
			settings.Converters.Add(new StructConverter());
			settings.DefaultValueHandling = DefaultValueHandling.Ignore;
			JsonSerializer serializer = JsonSerializer.Create(settings);
			serializer.Serialize(file, gameToStore);
		}

		currentFilePath = filePath;

	}

	private Dictionary<ISaveable, GameObjectData> savedObjects = new Dictionary<ISaveable, GameObjectData>();

	private PrefabData CreatePrefab(GameObject gameObject)
	{
		IncludeInSaveGame restore = gameObject.GetComponent<IncludeInSaveGame>();
		if (restore == null)
			return null;

		var prefabSave = new PrefabData();
		prefabSave.TemplateName = restore.PrefabName;

		if (restore.PersistPosition)
		{
			prefabSave.Position = gameObject.transform.position;
			prefabSave.Rotation = gameObject.transform.position;
		}

		ISaveable[] toBeSavedHere = gameObject.GetComponents<ISaveable>().Distinct().ToArray();

		var saveData = toBeSavedHere.Select(item => CreateSaveData(item)).ToList();
		prefabSave.GameObjects.AddRange(saveData);
		return prefabSave;
	}

	private GameObjectData CreateSaveData(ISaveable savable)
	{
		GameObjectData data;
		if (savedObjects.TryGetValue(savable,out data))
		{
			return data;
		}

		data = new GameObjectData()
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
			foreach (ISaveable savablePart in savableList) {
				GameObjectData item = CreateSaveData(saveableProperty);
				idList.Add(item.Id);
			}

			saveData.Links.Add(name, idList);
			return;
		}

		if (value is string || value is int || value is bool || value is float[,])
		{
			saveData.Attributes.Add(name, value);
			return;
		}

	}

	// Call this to load from a file into "data"
	public static void Load() { Load(currentFilePath); }   // Overloaded
	public static void Load(string filePath)
	{
		GameObjectData data = new GameObjectData();
		Stream stream = File.Open(filePath, FileMode.Open);
		BinaryFormatter bformatter = new BinaryFormatter();

		data = (GameObjectData)bformatter.Deserialize(stream);
		stream.Close();

		// Now use "data" to access your Values
	}

}
