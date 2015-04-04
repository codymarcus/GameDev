using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreenManager : MonoBehaviour {
	
	//public float waitTime;
	
	// Update is called once per frame
	void Update () {
		/*
		waitTime -= Time.deltaTime;
		if (waitTime <= 0){
			GameManager.ResetScores();
			Application.LoadLevel("Scene0");
		}
		*/
		if (Input.GetKeyDown ("space") || Input.GetButton ("Restart")) {
			GameManager.ResetScores();
			Application.LoadLevel ("StartMenu");
		}
		if (Input.GetKeyDown ("x") || Input.GetButton ("Back")) {
			GameManager.ResetScores();
			Application.LoadLevel ("Scene0");
		}

	}
}
