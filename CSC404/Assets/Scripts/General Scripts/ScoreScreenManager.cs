using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreenManager : MonoBehaviour {
	
	public float waitTime;
	
	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if (waitTime <= 0){
			GameManager.ResetScores();
			Application.LoadLevel("Scene0");
		}
		if (Input.GetKeyDown ("space") || Input.GetButton ("Fire2"))
			Application.LoadLevel ("Scene0");
		if (Input.GetKeyDown ("space") || Input.GetButton ("2Fire1"))
			Application.LoadLevel ("Scene0");

	}
}
