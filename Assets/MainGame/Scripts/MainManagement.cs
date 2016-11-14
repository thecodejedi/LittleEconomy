using UnityEngine;
using System.Collections;

public class MainManagement : MonoBehaviour
{
	public GameConfiguration configuration = null;

	public TerrainManager terrainManager;

	// Use this for initialization
	void Start()
	{
		GameObject gc = GameObject.FindWithTag("GameConfig");
		if (gc != null)
			configuration = gc.GetComponent<GameConfiguration>();

		if (configuration == null)
		{
			GameObject config = new GameObject("GameConfiguration");
			config.tag = "GameConfig";
			config.AddComponent(typeof(GameConfiguration));

			terrainManager.CreateNewTerrain();
			terrainManager.GenerateCities();
		}



	}

	// Update is called once per frame
	void Update()
	{

	}
}
