using UnityEngine;
using System.Collections;

public class PlayerReadyManager : MonoBehaviour {

	public GameObject PlayerNotReady;
	public GameObject PlayerReady;
	public int playerNumber;
	public static int isReady;
	bool personalReady;

	// Use this for initialization
	void Start () {
		PlayerReady.SetActive (false);
		PlayerNotReady.SetActive (true);
		isReady = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (isReady == 4) {
			//everyone is ready	
		} else {
			if (Input.GetKeyDown (playerNumber.ToString()) || Input.GetButtonDown ("Ready" + playerNumber)) {
				if (!personalReady) {
					PlayerNotReady.SetActive(false);
					PlayerReady.SetActive(true);
					personalReady = true;
					isReady++;
				} else {
					PlayerNotReady.SetActive(true);
					PlayerReady.SetActive(false);
					personalReady = false;
					isReady--;
				}
			}
		}
	}
}
