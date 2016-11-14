using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Button exitButton;
	public Button startButton;
	public GameConfiguration gameConfig;

	// Use this for initialization
	void Start () {
		startButton = startButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
	}
	
	public void ExitPress(){
		Application.Quit ();
	}

	public void StartPress(){
		DontDestroyOnLoad(gameConfig);
		SceneManager.LoadScene ("MainGame");
	}
}
