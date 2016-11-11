using UnityEngine;
using System.Collections;
using System;

public class Office : MonoBehaviour, ISaveable {

	[SaveGameInfo]
	public City city;

	[SaveGameInfo]
	public string state;

	public string TemplateName
	{
		get
		{
			return "Office";
		}
	}

	// Use this for initialization
	void Start () {
		city = city.GetComponent<City> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
