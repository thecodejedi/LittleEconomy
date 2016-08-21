using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CityMenu : MonoBehaviour {

	Canvas cityMenuCanvas;

	public GameObject cityName;

	City city;

	public GameObject resourceBuilding;

	public GameObject factoryBuilding;

	// Use this for initialization
	void Start () {
		cityMenuCanvas = GetComponent<Canvas> ();

		Close ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close(){
		cityMenuCanvas.enabled = false;
	}

	public void Open (City city)
	{
		cityMenuCanvas.enabled = true;
		cityName.GetComponent<Text> ().text = city.cityName;
		this.city = city;
	}

	public void AddResourceBuilding(string type){
		GameObject go = GameObject.Instantiate (resourceBuilding);
		ResourceBuilding rb = go.GetComponent<ResourceBuilding> ();
		rb.ResourceType = type;
		rb.transform.parent = city.gameObject.transform;
	}

	public void AddFactoryBuilding(){
		GameObject go = GameObject.Instantiate (factoryBuilding);
		FactoryBuilding fb = go.GetComponent<FactoryBuilding> ();
		fb.transform.parent = city.gameObject.transform;
	}
}
