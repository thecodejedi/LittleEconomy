using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {

	public Terrain terrain;

	[Range(1,10)]
	public float HM ;

	[Range(1,100)]
	public float divRange;

	[Range(0,100)]
	public float tileSize;

	public GameObject cityObject;

	// Use this for initialization
	void Start () {
		terrain = terrain.GetComponent<Terrain> ();
		GenerateTerrain (terrain,1f);
		GenerateCities ();
	}

	void GenerateCities ()
	{
		float x = terrain.terrainData.size.x;
		float z = terrain.terrainData.size.z;
		System.Random rnd = new System.Random ();
		Debug.LogWarning("TerrainSize: x" + x +"z:" + z);
		for (int i = 0; i < 1000; i++) {
			int xPos = rnd.Next (0, (int)x);
			int zPos = rnd.Next (0, (int)z);
			GameObject copy = GameObject.Instantiate(cityObject);
			copy.layer = 9;
			copy.transform.position = new Vector3 (xPos, 1000, zPos);
			City city = copy.GetComponent<City> ();
			city.cityName = i.ToString();
			AlignWithGround (city);
		}

	}

	static void AlignWithGround (City city) {
		GameObject obj = city.texture;

		Transform myTransform = obj.transform;

			RaycastHit hit;
			if (Physics.Raycast (myTransform.position, -Vector3.up, out hit)) {
				Vector3 targetPosition = hit.point;
				if (myTransform.gameObject.GetComponent<MeshFilter>() != null) {
					Bounds bounds = myTransform.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds;
					targetPosition.y += bounds.extents.y;
				}
				myTransform.position = targetPosition;
				Vector3 targetRotation = new Vector3 (hit.normal.x, myTransform.eulerAngles.y, hit.normal.z);
			myTransform.eulerAngles = targetRotation;
		}
	}

	//Our Generate Terrain function
	public void GenerateTerrain(Terrain t, float tileSize)
	{

		//Heights For Our Hills/Mountains
		float[,] hts = new float[t.terrainData.heightmapWidth, t.terrainData.heightmapHeight];
		for (int i = 0; i < t.terrainData.heightmapWidth; i++)
		{
			for (int k = 0; k < t.terrainData.heightmapHeight; k++)
			{
				hts[i, k] = Mathf.PerlinNoise(((float)i / (float)t.terrainData.heightmapWidth) * tileSize, ((float)k / (float)t.terrainData.heightmapHeight) * tileSize)/ divRange;
			}
		}
		Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
		t.terrainData.SetHeights(0, 0, hts);
	}

}
