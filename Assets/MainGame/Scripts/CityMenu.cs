using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CityMenu : MonoBehaviour {

	Canvas cityMenuCanvas;

	public GameObject cityName;

	public Dropdown factoryDropwDown;

	City city;

	public GameObject resourceBuilding;

	public GameObject factoryBuilding;

	public FactoryMenu factoryMenu;

	// Use this for initialization
	void Start () {
		cityMenuCanvas = GetComponent<Canvas> ();
		factoryDropwDown = factoryDropwDown.GetComponent<Dropdown> ();
		factoryMenu = factoryMenu.GetComponent<FactoryMenu> ();
		Close ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close(){
		cityMenuCanvas.enabled = false;
	}

	public void FactorySelected(Int32 pos){
		factoryMenu.Open (factories [pos]);
	}

	IList<FactoryBuilding> factories;

	public void Open (City city)
	{
		cityMenuCanvas.enabled = true;
		cityName.GetComponent<Text> ().text = city.cityName;
		this.city = city;
		factories = city.GetComponentsInChildren<FactoryBuilding> ();

		List<Dropdown.OptionData> options = new List<Dropdown.OptionData> ();
		foreach (FactoryBuilding fb in factories) {
			options.Add (new Dropdown.OptionData (fb.Name));
		}

		factoryDropwDown.ClearOptions ();
		factoryDropwDown.AddOptions (options);
	}

	public void AddResourceBuilding(string type){
		GameObject go = GameObject.Instantiate (resourceBuilding);
		ResourceBuilding rb = go.GetComponent<ResourceBuilding> ();
		rb.ResourceType = type;
		rb.name = "Resource " + currName++;
		rb.transform.parent = city.gameObject.transform;
		Open (city);
	}

	int currName = 0;

	public void AddFactoryBuilding(){
		GameObject go = GameObject.Instantiate (factoryBuilding);
		FactoryBuilding fb = go.GetComponent<FactoryBuilding> ();
		fb.name = "Factory " + currName++;
		fb.transform.parent = city.gameObject.transform;
		Open (city);
	}
}
