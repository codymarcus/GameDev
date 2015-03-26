using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2"))
			Application.LoadLevel ("Scene0");
	}
}
