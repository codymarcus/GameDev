using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour {
	
	public Image fadeout;
	float alpha = 0.0f;

	// Update is called once per frame
	void Update () {
		if (PlayerReadyManager.isReady == 4) {
			alpha += 0.01f;
			Debug.Log (alpha);
			fadeout.color = new Color(fadeout.color[0], fadeout.color[1], fadeout.color[2], alpha);
			if(alpha >= 1.0) {
				Application.LoadLevel("scene0");
			}
		}
	}
}
