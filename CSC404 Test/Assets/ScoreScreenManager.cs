using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreenManager : MonoBehaviour {

	public static int[] matchScores = {0, 0, 0, 0};
	string displayText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		displayText = "Player 1: " + matchScores [0] + "\nPlayer 2: " + matchScores [1] + "\nPlayer 3: "
						+ matchScores [2] + "\nPlayer 4: " + matchScores [3];
		GameObject.FindGameObjectWithTag ("Scores").GetComponent<Text> ().text = displayText;
	}
}
