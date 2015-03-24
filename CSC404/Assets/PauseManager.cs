using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

	public GameObject Pause_image;

	// Update is called once per frame
	void Update () {
		bool isPaused = GameManager.paused;
		if (isPaused) 
			Pause_image.SetActive (true);
		else
			Pause_image.SetActive (false);
	}
}
