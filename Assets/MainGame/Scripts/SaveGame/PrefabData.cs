using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistQuaternion
{

	public PersistQuaternion()
	{
	}

	public PersistQuaternion(Quaternion qaternion)
	{
		this.x = qaternion.x;
		this.y = qaternion.y;
		this.z = qaternion.z;
		this.w = qaternion.w;
	}


	public Quaternion ToQuaternion()
	{
		return new Quaternion(x, y, z, w);
	}

	public float x;

	public float y;

	public float z;

	public float w;

}

public class PersistVector3
{
	public PersistVector3()
	{
	}

	public PersistVector3(Vector3 vector3)
	{
		this.x = vector3.x;
		this.y = vector3.y;
		this.z = vector3.z;
	}

	public float x;
	public float y;
	public float z;

	public Vector3 ToVector3()
	{
		return new Vector3(x, y, z);
	}
}

public class PrefabData
{

	public string TemplateName { get; set; }

	public PrefabData()
	{
	}


	public PersistVector3 Position { get; set; }

	public PersistQuaternion Rotation { get; set; }

	public PersistVector3 Scale { get; set; }

	public List<GameObjectData> GameObjects = new List<GameObjectData>();
}