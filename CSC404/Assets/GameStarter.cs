using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour {
	
	public Image fadeout;
	public GameObject notice; 
	float alpha = 0.0f;
	float notice_time = 0.0f;
	public static bool start= false;

	void Start() {
		start = false;
	} 

	// Update is called once per frame
	void Update () {

		if (notice_time > 0.0) {
			notice_time-=0.1f;		
		} else {
			notice.SetActive(false);
		}

		if (!start) {
			if (PlayerReadyManager.isReady >= 2) {
				if (Input.GetKeyDown ("z") || Input.GetButtonDown ("Fire4")) {	
					start = true;
				}
			} else {
				if (Input.GetKeyDown ("z") || Input.GetButtonDown ("Fire4")) {	
					notice.SetActive(true);
					notice_time = 6.0f;
				}
			}
		} else {
			Debug.Log (1111);
			for (int i=0;i<4;i++) {
				Debug.Log(PlayerReadyManager.players[i]);
			}
			Debug.Log (2222);
			alpha += 0.01f;
			fadeout.color = new Color(fadeout.color[0], fadeout.color[1], fadeout.color[2], alpha);
			if(alpha >= 1.0) {
				Application.LoadLevel("scene0");
			}
		}
	}
}
