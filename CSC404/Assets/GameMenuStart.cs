using UnityEngine;
using System.Collections;

public class GameMenuStart : MonoBehaviour {

	public void QuickMatch() {
		Application.LoadLevel();
	}

	public void Tournament() {
		Application.LoadLevel();
	}

	public void EndGame() {
		Application.Quit();
	}
}
