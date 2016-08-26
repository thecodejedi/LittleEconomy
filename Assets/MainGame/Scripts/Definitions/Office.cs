using UnityEngine;
using System.Collections;

public class Office : MonoBehaviour {

	public City city;

	public string state;

	// Use this for initialization
	void Start () {
		city = city.GetComponent<City> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
