using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FactoryMenu : MonoBehaviour
{

	Canvas factoryMenuCanvas;

	public Dropdown resourcesDropDown;

	public GameObject factoryName;

	IList<ResourceBuilding> resources;

	private FactoryBuilding factory;
	public void Open(FactoryBuilding factory){
		this.factory = factory;
		factoryMenuCanvas.enabled = true;

		factoryName.GetComponent<Text> ().text = factory.name;

		resources = new List<ResourceBuilding>();
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("CalcRoot");
		foreach (GameObject obj in objs) {
			ResourceBuilding rb = obj.GetComponent<ResourceBuilding> ();
				if(rb != null)
				resources.Add (rb);
		}

		List<Dropdown.OptionData> options = new List<Dropdown.OptionData> ();
		foreach (ResourceBuilding rb in resources) {
			options.Add (new Dropdown.OptionData (rb.Name));
		}

		resourcesDropDown.ClearOptions ();
		resourcesDropDown.AddOptions (options);
	}

	public void ResourceSelected(Int32 pos){
		ResourceBuilding building =  resources [pos];
		if(!factory.Dependencies.Contains(building))
			factory.Dependencies.Add (building);
	}

	// Use this for initialization
	void Start () {
		factoryMenuCanvas = GetComponent<Canvas> ();
		resourcesDropDown = resourcesDropDown.GetComponent<Dropdown> ();
		Close ();
	}

	public void Close(){
		factoryMenuCanvas.enabled = false;
	}
}

