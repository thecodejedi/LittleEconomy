using System;
using System.Collections.Generic;

public class SavePrefab
	{

		public string TemplateName { get; set; }

		public SavePrefab()
		{
		}

		public List<SaveData> DataPoints = new List<SaveData>();
	}