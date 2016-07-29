using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateMap : MonoBehaviour {

	public int width;
	public int height;

	int[,] map;

	void Generate ()
	{
		map = new int[width,height];
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {

					map[x,y] = 1;
			}
		}
		MeshGenerator meshGen = GetComponent<MeshGenerator>();
		meshGen.GenerateMesh(map, 1);
	}


	// Use this for initialization
	void Start () {
		Generate ();
	}

	void OnDrawGizmos() {
        if (map != null) {
            for (int x = 0; x < width; x ++) {
                for (int y = 0; y < height; y ++) {
                    Gizmos.color = (map[x,y] == 1)?Color.black:Color.white;
                    Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);
                    Gizmos.DrawCube(pos,Vector3.one);
                }
            }
        }
	}

}
