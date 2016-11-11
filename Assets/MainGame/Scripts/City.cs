using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class City : MonoBehaviour, ISaveable {

	[SaveGameInfo]
	public string cityName;

	[SaveGameInfo]
	public IList<Office> Offices = new List<Office>();

	[SaveGameInfo]
	public Text text;

	public GameObject texture;

	public Canvas cityMenuCanvas;

	CityMenu menu;

	public string TemplateName
	{
		get
		{
			return "City";
		}
	}

	// Use this for initialization
	void Start () {
		text = text.GetComponent<Text> ();
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
