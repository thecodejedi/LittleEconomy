using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CityMenu : MonoBehaviour {

	public Canvas cityMenuCanvas;

	public Canvas defaultOverlay;

	public GameObject cityName;

	public Dropdown factoryDropwDown;

	City city;

	public GameObject resourceBuilding;

	public GameObject factoryBuilding;

	public FactoryMenu factoryMenu;

	private List<OfficeButton> officeButtons = new List<OfficeButton>();

	// Use this for initialization
	void Start () {
		cityMenuCanvas = cityMenuCanvas.GetComponent<Canvas> ();
		factoryDropwDown = factoryDropwDown.GetComponent<Dropdown> ();
		factoryMenu = factoryMenu.GetComponent<FactoryMenu> ();
		defaultOverlay = defaultOverlay.GetComponent<Canvas> ();
		Close();
		officeButtons.AddRange (GetComponentsInChildren<OfficeButton> ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close(){
		this.enabled = false;
		cityMenuCanvas.enabled = false;
		defaultOverlay.enabled = true;
	}

	public void FactorySelected(Int32 pos){
		factoryMenu.Open (factories [pos]);
	}

	IList<FactoryBuilding> factories;

	public void Open (City city)
	{
		this.enabled = true;
		cityMenuCanvas.enabled = true;
		defaultOverlay.enabled = false;
		cityName.GetComponent<Text> ().text = city.cityName;
		this.city = city;
		factories = city.GetComponentsInChildren<FactoryBuilding> ();

		List<Dropdown.OptionData> options = new List<Dropdown.OptionData> ();
		foreach (FactoryBuilding fb in factories) {
			options.Add (new Dropdown.OptionData (fb.Name));
		}

		factoryDropwDown.ClearOptions ();
		factoryDropwDown.AddOptions (options);

		for (int i = 0; i < officeButtons.Count; i++) {
			OfficeButton button = officeButtons [i];
			if (city.Offices.Count <= i)
				break;
			button.SetOffice (city.Offices [i]);
		}
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
