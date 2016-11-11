using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class StructConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType.IsAssignableFrom(typeof(Vector3));
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{

		// Load JObject from stream
		JObject jObject = JObject.Load(reader);

		// Create target object based on JObject
		float x = float.Parse(jObject["X"].ToString());
		float y = float.Parse(jObject["Y"].ToString());
		float z = float.Parse(jObject["Z"].ToString());

		var target = new Vector3(x, y, z);
		// Populate the object properties
		serializer.Populate(jObject.CreateReader(), target);
		return target;
	}

	public override void WriteJson(
		JsonWriter writer, object value, JsonSerializer serializer)
	{
		var myObject = (Vector3)value;
		var jObject = new JObject();
		jObject.Add("X", myObject.x);
		jObject.Add("Y", myObject.y);
		jObject.Add("Z", myObject.z);

		serializer.Serialize(writer, jObject);
	}
}
