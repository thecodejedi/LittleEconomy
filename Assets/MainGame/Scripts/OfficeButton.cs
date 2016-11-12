using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OfficeButton : MonoBehaviour {
	
	public Office office;

	public Button button;

	public Text state;

	public void ButtonClicked(){
		Debug.LogWarning ("Office Button clicked");
		Debug.LogWarning (button.name);
	}
		

	public void SetOffice(Office office){
		this.office = office;
		state.text = office.state;
	}

	// Use this for initialization
	void Start () {
		button = button.GetComponent<Button> ();
		state = state.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
