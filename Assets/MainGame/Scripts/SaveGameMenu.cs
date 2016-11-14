using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SaveGameMenu : MonoBehaviour
{

	public Canvas saveMenuCanvas;

	public Canvas gameOverlay;

	public Hibernator hibernator;

	public InputField inputField;

	public Button saveGameButton;

	// Use this for initialization
	void Start()
	{
		

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Show()
	{
		IEnumerable<string> saveGames = hibernator.LoadSaveGames();

		gameOverlay.enabled = false;
		saveMenuCanvas.enabled = true;
	}

	public void Hide()
	{
		gameOverlay.enabled = true;
		saveMenuCanvas.enabled = false;
	}

	public void SaveButton(Button b)
	{
		return;
	}

	public void Save()
	{
		if (!string.IsNullOrEmpty(inputField.text))
		{
			hibernator.Save(inputField.text);
			Hide();
		}

	}
}
