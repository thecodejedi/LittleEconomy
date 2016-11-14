using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGameMenu : MonoBehaviour {

	public Canvas loadCanvas;

	public Canvas gameOverlay;

	public MainManagement mainManagement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show()
	{
		loadCanvas.enabled = true;
	}

	public void Hide()
	{

		loadCanvas.enabled = false;
	}

	public void LoadGame()
	{
		mainManagement.configuration.saveGame = Hibernator.currentFilePath;
		DontDestroyOnLoad(mainManagement.configuration);
		SceneManager.LoadScene("MainGame");
	}
}
