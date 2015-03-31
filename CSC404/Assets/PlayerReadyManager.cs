using UnityEngine;
using System.Collections;

public class PlayerReadyManager : MonoBehaviour {

	public GameObject PlayerReadyText;
	public GameObject PlayerReady;
	public int playerNumber;
	public static int isReady;
	public static int[] players = {0,0,0,0};
	bool personalReady;

	// Use this for initialization
	void Start () {
		PlayerReady.SetActive (false);
		PlayerReadyText.SetActive (false);
		isReady = 0;
		for (int i=0; i<4; i++) {
			players[i] = 0;		
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameStarter.start) {
			if (Input.GetKeyDown (playerNumber.ToString()) || Input.GetButtonDown ("Ready" + playerNumber)) {
				if (!personalReady) {
					PlayerReadyText.SetActive(true);
					PlayerReady.SetActive(true);
					personalReady = true;
					isReady++;
					players[playerNumber-1] = 1;
				} else {
					PlayerReadyText.SetActive(false);
					PlayerReady.SetActive(false);
					personalReady = false;
					isReady--;
					players[playerNumber-1] = 0;
				}
			}
		} 
	}
}
