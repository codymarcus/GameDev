using UnityEngine;
using System.Collections;

public class ShowStartTime : MonoBehaviour {

	public float gameStartTime = 3.0f;

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;

	// Update is called once per frame
	void Update () {
		gameStartTime -= Time.deltaTime;
		if (gameStartTime > 2.0) {
			//Debug.Log(gameStartTime);
			text1.SetActive(true);
			text2.SetActive(false);
			text3.SetActive(false);
			text4.SetActive (false);
		}else if (gameStartTime < 2.0 && gameStartTime > 1.0) {
			text2.SetActive(true);
			text1.SetActive(false);
		}else if (gameStartTime < 1.0 && gameStartTime > 0.0) {
			text2.SetActive(false);
			text3.SetActive(true);
		}else if (gameStartTime < 0.0 && gameStartTime > -1.0) {
			text3.SetActive(false);
			text4.SetActive (true);
		}else if(gameStartTime < -1.0) {
			text4.SetActive(false);
		}

						
	}
}
