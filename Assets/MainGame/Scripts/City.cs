﻿using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {

	public string cityName;

	public TextMesh text;

	public GameObject texture;

	public Canvas cityMenuCanvas;
	CityMenu menu;

	// Use this for initialization
	void Start () {
		text = text.GetComponent<TextMesh> ();
		text.text = cityName;
		menu = cityMenuCanvas.GetComponent<CityMenu> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown() {
		Debug.LogWarning ("City " + cityName + " clicked");
		menu.Open (this);
	}
}
