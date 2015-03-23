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
	}
}
