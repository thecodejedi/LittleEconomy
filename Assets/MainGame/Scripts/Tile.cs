using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
	public IEnumerable<Vector3> CreateVector ()
	{
		yield return new Vector3 (x, y);
		yield return new Vector3 (x+1, y);
		yield return new Vector3 (x+1, y+1);
		yield return new Vector3 (x, y+1);
		yield return new Vector3 (x, y);

	}

	public int x;

	public int y;

	public Tile (int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}

