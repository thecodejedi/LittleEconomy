using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {

	public Terrain terrain;

	[Range(1,10)]
	public float HM ;

	[Range(1,100)]
	public float divRange;

	[Range(0,100)]
	public float tileSize;

	Object officeObject;

	Object cityObject;

	public GameObject cityMenuObject;

	// Use this for initialization
	void Start () {
		terrain = terrain.GetComponent<Terrain> ();
		GenerateTerrain (terrain,tileSize);

		officeObject = Resources.Load("Prefabs/Office");
		cityObject = Resources.Load("Prefabs/DefaultCity");

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
			GameObject copy = (GameObject)GameObject.Instantiate(cityObject,new Vector3(xPos, 1000, zPos),Quaternion.identity);
			SetLayerRecursively (copy, 9);
			copy.name = "City " + i;
			City city = copy.GetComponent<City> ();
			city.cityName = i.ToString();
			city.cityMenuObject = cityMenuObject;
			city.Offices = GenerateOffices (city);
			AlignWithGround (city);
		}

	}

	IList<Office> GenerateOffices(City city){
		IList<Office> result = new List<Office>();
		for(int i = 0; i<12;i++){
			GameObject officeCopy =(GameObject)GameObject.Instantiate(officeObject, Vector3.zero, Quaternion.identity);
			Office office = officeCopy.GetComponent<Office>();
			office.state = "bla";
			office.city = city;
			result.Add(officeCopy.GetComponent<Office>());
		}
		return result;
	}

	void SetLayerRecursively(GameObject obj, int newLayer)
	{
		if (null == obj)
		{
			return;
		}

		obj.layer = newLayer;

		foreach (Transform child in obj.transform)
		{
			if (null == child)
			{
				continue;
			}
			SetLayerRecursively(child.gameObject, newLayer);
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
				hts[i, k] = Mathf.PerlinNoise(((float)i / (float)t.terrainData.heightmapWidth) * tileSize, ((float)k / (float)t.terrainData.heightmapHeight) * tileSize)/ divRange*HM;
			}
		}
		Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
		t.terrainData.SetHeights(0, 0, hts);


		Smooth();
	}

	private void Smooth()
	{

		float[,] height = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth,
										  terrain.terrainData.heightmapHeight);
		float k = 0.5f;
		/* Rows, left to right */
		for (int x = 1; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = 0; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x - 1, z] * (1 - k) +
						  height[x, z] * k;

		/* Rows, right to left*/
		for (int x = terrain.terrainData.heightmapWidth - 2; x < -1; x--)
			for (int z = 0; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x + 1, z] * (1 - k) +
						  height[x, z] * k;

		/* Columns, bottom to top */
		for (int x = 0; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = 1; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x, z - 1] * (1 - k) +
						  height[x, z] * k;

		/* Columns, top to bottom */
		for (int x = 0; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = terrain.terrainData.heightmapHeight; z < -1; z--)
				height[x, z] = height[x, z + 1] * (1 - k) +
						  height[x, z] * k;

		terrain.terrainData.SetHeights(0, 0, height);
	}

}
