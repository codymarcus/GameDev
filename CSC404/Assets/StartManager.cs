using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space"))
			Application.LoadLevel ("Scene0");
	}
}
