using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	public GameObject readyPrompt;

	void Start () {
		readyPrompt.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2"))
			//Application.LoadLevel ("Scene0");
			readyPrompt.SetActive (true);
	}
}
