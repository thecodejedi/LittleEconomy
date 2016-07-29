using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManageMainOverlay : MonoBehaviour {

	public Canvas mainMenuCanvas;

	// Use this for initialization
	void Start () {
		mainMenuCanvas = mainMenuCanvas.GetComponent<Canvas> ();
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
}
