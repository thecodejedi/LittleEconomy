﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManageMainOverlay : MonoBehaviour {

	public Canvas mainMenuCanvas;

	public Canvas saveCanvas;

	public GameObject defaultOverlay;

	// Use this for initialization
	void Start () {
		mainMenuCanvas = mainMenuCanvas.GetComponent<Canvas> ();
		defaultOverlay.SetActive (true);
		mainMenuCanvas.gameObject.SetActive (true);
		HideMenu ();
	}

	public void DisplayMenu(){
		
		mainMenuCanvas.enabled = true;
	}

	public void HideMenu(){
		mainMenuCanvas.enabled = false;
	}

	public void ReturnToMainMenu(){
		SceneManager.LoadScene ("StartMenu");
	}

	public void ExitToDesktop(){
		Application.Quit();
	}

	public void QuickSave()
	{
		var saveLoad = saveCanvas.GetComponent<Hibernator>();
		saveLoad.Save();
	}
}
