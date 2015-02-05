using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchManager : MonoBehaviour {

	public static int roundNumber = 0;
	public string[] gameTypes = {"Last Man Standing", "Last Team Standing"};
	public static string gameType;

	// Use this for initialization
	void Start () {
		// Set Round Number Text
		GameObject.FindGameObjectWithTag ("Round").GetComponent<Text> ().text = "Round " + roundNumber;

		// Select random GameType and set GameType text
		int i = Random.Range (0, gameTypes.Length);
		gameType = gameTypes [i];
		GameObject.FindGameObjectWithTag ("GameType").GetComponent<Text> ().text = gameType;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
