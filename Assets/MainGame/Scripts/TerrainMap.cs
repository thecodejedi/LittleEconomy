using UnityEngine;
using System.Collections;

public class TerrainMap : MonoBehaviour, ISaveable {

	public Terrain terrain;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[SaveGameValue]
	public float[,] heights
	{
		get
		{
			return terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth,
										  terrain.terrainData.heightmapHeight);
		}
		set
		{
			terrain.terrainData.SetHeights(0, 0, value);
		}
	}
}
