using UnityEngine;    // For Debug.Log, etc.

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections.Generic;

public class GameObjectData
{
	private static int id = 1;

	public int Id { get; set;}

	public GameObjectData(bool forSave) :this()
	{
		lock (typeof(GameObjectData))
			this.Id = id++;
	}

	public GameObjectData() {
		Attributes = new Dictionary<string, object>();
		Floats = new Dictionary<string, float[,]>();
		Links = new Dictionary<string, List<int>>();
	}

	public string TypeName { get; set; }

	public string Name { get; set; }

	public Dictionary<string, object> Attributes { get; set; } 

	public Dictionary<string, float[,]> Floats { get; set; }

	public Dictionary<string, List<int>> Links { get; set; }


}


