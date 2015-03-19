using UnityEngine;
using System.Collections;

public class StartColision : MonoBehaviour {

	public float gameStartTime = 3.0f;

	// Update is called once per frame
	void Update () {
		gameStartTime -= Time.deltaTime;
		if (gameStartTime < 0.0) {
			Destroy (gameObject);
		}
	}
}
