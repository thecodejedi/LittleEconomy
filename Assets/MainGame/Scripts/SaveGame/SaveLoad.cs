using System;
using UnityEngine;    // For Debug.Log, etc.

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Runtime.Serialization;
using System.Reflection;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Collections.Generic;


// === This is the class that will be accessed from scripts ===
public class SaveLoad
{

	public static string currentFilePath = "SaveData.cjc";    // Edit this for different save files

	// Call this to write data
	public void Save()  // Overloaded
	{
		Save(currentFilePath);
	}
	public void Save(string filePath)
	{
		var objectsToSave = GameObject.FindGameObjectsWithTag("saveObject");

		SaveGame gameToStore = new SaveGame();
		var saveDatas = objectsToSave.Select(item => CreateSaveData(item));

		gameToStore.DataPoints.AddRange(saveDatas);

	}

	private Dictionary<ISaveable, int> savedObjects = new Dictionary<ISaveable, int>();

	private SaveData CreateSaveData(GameObject gameObject)
	{
		ISaveable toBeSavedHere = gameObject.GetComponent<ISaveable>();
		return CreateSaveData(toBeSavedHere);
	}

	private SaveData CreateSaveData(ISaveable savable)
	{

		SaveData data = new SaveData()
		{
			TypeName = savable.GetType().FullName,
			TemplateName = savable.TemplateName
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

			if (value is string || value is int || value is bool)
			{
				saveData.Attributes.Add(info.Name, value.ToString());
				continue;
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

				saveData.Links.Add(info.Name, id);
				continue;
			}

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
