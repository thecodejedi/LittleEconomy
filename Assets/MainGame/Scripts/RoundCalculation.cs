using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundCalculation : MonoBehaviour {

	public int RoundNumber;

	public GameObject calculationRoot;
	public GameObject resourceBatch;

	// Use this for initialization
	void Start () {
		calculationRoot = calculationRoot.GetComponent<GameObject> ();
	}
	
	public void NextRound(){
		IList<GameObject> calculatables = GameObject.FindGameObjectsWithTag ("CalcRoot");
		foreach (GameObject go in calculatables) {
			ICalculatable calc = go.GetComponent<ICalculatable> ();
			Debug.LogWarning ("Pulling " + calc.Name);
			calc.Pull ();
		}

		foreach (GameObject go in calculatables) {
			ICalculatable calc = go.GetComponent<ICalculatable> ();
			Debug.LogWarning ("Processing " + calc.Name);
			calc.Process ();
		}
	}
}
