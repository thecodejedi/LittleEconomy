using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CityMenu : MonoBehaviour {

	Canvas cityMenuCanvas;

	public GameObject cityName;


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
	}
}
