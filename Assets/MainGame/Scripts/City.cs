using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class City : MonoBehaviour, ISaveable {

	[SaveGameValue]
	public string cityName;

	[SaveGameValue]
	public IList<Office> Offices = new List<Office>();

	public Text text;

	public GameObject texture;

	// Use this for initialization
	void Start () {
		text.text = cityName;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		Debug.LogWarning ("City " + cityName + " clicked");
		CityMenu cityMenu = GameObject.Find("CityMenuCanvas").GetComponent<CityMenu>();
		cityMenu.Open (this);
	}
}
