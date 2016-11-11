using UnityEngine;    // For Debug.Log, etc.

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections.Generic;

public class SaveData
{
	private static int id;

	public int Id { get; private set;}

	public SaveData() {
		lock(typeof(SaveData))
			this.Id = id++;
	}

	public string TypeName { get; set; }

	public string TemplateName { get; set; }

	public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

	public Dictionary<string, int> Links { get; set; } = new Dictionary<string, int>();

}


