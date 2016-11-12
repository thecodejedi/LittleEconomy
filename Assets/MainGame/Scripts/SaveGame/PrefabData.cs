using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabData
	{

		public string TemplateName { get; set; }

		public PrefabData()
		{
		}


	public Vector3 Position { get; set; }

	public Vector3 Rotation { get; set; }

	public Vector3 Scale { get; set; }

		public List<GameObjectData> GameObjects = new List<GameObjectData>();
	}