using UnityEngine;
using System.Collections;
using System;

public class Office : MonoBehaviour, ISaveable {

	[SaveGameValue]
	public City city;

	[SaveGameValue]
	public string state;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
