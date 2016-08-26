using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class City : MonoBehaviour {

	public string cityName;

	public IList<Office> Offices = new List<Office>();

	public Text text;

	public GameObject texture;

	public Canvas cityMenuCanvas;
	CityMenu menu;

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
