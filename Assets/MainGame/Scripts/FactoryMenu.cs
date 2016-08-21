using System;
using UnityEngine;
using UnityEngine.UI;

public class FactoryMenu : MonoBehaviour
{

	Canvas factoryMenuCanvas;

	public GameObject factoryName;

	private FactoryBuilding factory;
	public void Open(FactoryBuilding factory){
		this.factory = factory;
		factoryMenuCanvas.enabled = true;

		factoryName.GetComponent<Text> ().text = factory.name;
	}

	// Use this for initialization
	void Start () {
		factoryMenuCanvas = GetComponent<Canvas> ();
		Close ();
	}

	public void Close(){
		factoryMenuCanvas.enabled = false;
	}
}

