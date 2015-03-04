using UnityEngine;
using System.Collections;

public class GameMenuStart : MonoBehaviour {

	public void QuickMatch() {
		Application.LoadLevel("GameMenu");
	}

	public void Tournament() {
		Application.LoadLevel("RoundScreen");
	}

	public void EndGame() {
		Application.Quit();
	}
}
