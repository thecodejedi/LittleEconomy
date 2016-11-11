﻿using System;
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

public class SaveLoad : MonoBehaviour
{

	public Canvas saveCanvas;

	void Start()
	{
	}

	void Update()
	{
		
	}


	public static string currentFilePath = "SaveData.cjc";    // Edit this for different save files


	public void Save()
	{
		Save(currentFilePath);

	}


	public void Save(string filePath)
	{
		saveCanvas.enabled = true;
		var objectsToSave = UnityEngine.Object.FindObjectsOfType<GameObject>();

		SaveGame gameToStore = new SaveGame();
		var saveDatas = objectsToSave.Select(item => CreatePrefab(item)).Where(item => item != null);

		gameToStore.DataPoints.AddRange(saveDatas);
		// serialize JSON directly to a file
		using (StreamWriter file = File.CreateText(@"SaveData.cjc"))
		{
			var settings = new JsonSerializerSettings();
			settings.Formatting = Formatting.Indented;
			settings.Converters.Add(new StructConverter());
			JsonSerializer serializer = JsonSerializer.Create(settings);
			serializer.Serialize(file, gameToStore);
		}

		saveCanvas.enabled = false;



	}

	private Dictionary<ISaveable, int> savedObjects = new Dictionary<ISaveable, int>();

	private SavePrefab CreatePrefab(GameObject gameObject)
	{
		RestorePrefab restore = gameObject.GetComponent<RestorePrefab>();
		if (restore == null)
			return null;

		var prefabSave = new SavePrefab();
		prefabSave.TemplateName = restore.PrefabName;

		ISaveable[] toBeSavedHere = gameObject.GetComponents<ISaveable>();

		var saveData = toBeSavedHere.Select(item=>CreateSaveData(item));
		prefabSave.DataPoints.AddRange(saveData);
		return prefabSave;
	}

	private SaveData CreateSaveData(ISaveable savable)
	{
		if (savedObjects.ContainsKey(savable))
		{
			return null;
		}

		SaveData data = new SaveData()
		{
			TypeName = savable.GetType().FullName,
		};

		savedObjects.Add(savable, data.Id);

		FillSaveData(savable, data);

		return data;
	}

	private void FillSaveData(ISaveable saveable, SaveData saveData)
	{
		var propertiesToSave = saveable.GetType().GetProperties().Where(property => property.GetCustomAttributes(false).OfType<SaveGameInfoAttribute>().Any()).ToList();

		foreach (PropertyInfo info in propertiesToSave)
		{
			if (!info.CanRead)
			{
				continue;
			}

			var value = info.GetValue(saveable, null);
			PersistValue(saveData, info.Name, value);

		}

		var fieldsToSave = saveable.GetType().GetFields().Where(property => property.GetCustomAttributes(false).OfType<SaveGameInfoAttribute>().Any()).ToList();

		foreach (FieldInfo info in fieldsToSave)
		{
			var value = info.GetValue(saveable);
			PersistValue(saveData, info.Name, value);

		}

	}

	private void PersistValue(SaveData saveData, string name, object value)
	{
		if (value is string || value is int || value is bool)
		{
			saveData.Attributes.Add(name, value.ToString());
			return;
		}

		ISaveable saveableProperty = value as ISaveable;

		if (saveableProperty != null)
		{
			int id;
			if (!savedObjects.TryGetValue(saveableProperty, out id))
			{
				SaveData item = CreateSaveData(saveableProperty);
				id = item.Id;
			}

			saveData.Links.Add(name, new List<int> { id });
			return;
		}

		IEnumerable<ISaveable> savableList = value as IEnumerable<ISaveable>;
		if (savableList != null)
		{
			var idList = new List<int>();
			foreach (ISaveable savablePart in savableList)
			{
				int id;
				if (!savedObjects.TryGetValue(saveableProperty, out id))
				{
					SaveData item = CreateSaveData(saveableProperty);
					id = item.Id;
					idList.Add(id);
				}


			}

			saveData.Links.Add(name, idList);

		}

	}

	// Call this to load from a file into "data"
	public static void Load() { Load(currentFilePath); }   // Overloaded
	public static void Load(string filePath)
	{
		SaveData data = new SaveData();
		Stream stream = File.Open(filePath, FileMode.Open);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder();
		data = (SaveData)bformatter.Deserialize(stream);
		stream.Close();

		// Now use "data" to access your Values
	}

}
