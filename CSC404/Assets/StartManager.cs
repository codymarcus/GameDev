using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	public GameObject[] readyPrompt;

	// Update is called once per frame
	void Update () {
		if (PlayerReadyManager.isReady >= 2) {
			if (Input.GetKeyDown ("z") || Input.GetButtonDown ("Fire4")) {	
				Application.LoadLevel("scene0");
			}
		}
	}
}
