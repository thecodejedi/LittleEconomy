using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManageMainOverlay : MonoBehaviour {

	public Canvas mainMenuCanvas;

	public SaveGameMenu saveGameMenu;

	public LoadGameMenu loadGameMenu;

	public Hibernator hibernator;

	public Canvas defaultOverlay;

	// Use this for initialization
	void Start () {
		mainMenuCanvas = mainMenuCanvas.GetComponent<Canvas> ();
		mainMenuCanvas.gameObject.SetActive (true);
		HideMenu ();
	}

	public void DisplayMenu(){
		defaultOverlay.enabled = false;
		mainMenuCanvas.enabled = true;
	}

	public void HideMenu(){
		mainMenuCanvas.enabled = false;
		defaultOverlay.enabled = true;
	}

	public void ReturnToMainMenu(){
		SceneManager.LoadScene ("StartMenu");
	}

	public void ExitToDesktop(){
		Application.Quit();
	}

	public void QuickSave()
	{
		hibernator.QuickSave();
		HideMenu();
	}

	public void Save()
	{
		HideMenu();
		saveGameMenu.Show();
	}

	public void Load()
	{
		HideMenu();
		loadGameMenu.Show();
	}
}
