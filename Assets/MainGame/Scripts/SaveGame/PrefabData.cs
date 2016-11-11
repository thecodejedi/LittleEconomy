using System;
using System.Collections.Generic;

public class PrefabData
	{

		public string TemplateName { get; set; }

		public PrefabData()
		{
		}

		public List<GameObjectData> GameObjects = new List<GameObjectData>();
	}