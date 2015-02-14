using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour {

	public static int roundNumber = 1;
	public string[] gameTypes = {"Last Man Standing", "Last Team Standing", "King of the Hill", "WANTED"};
	public static string gameType;
	public string[] possibleTeamDynamics = {"FFA", "2v2"};
	public static string teamDynamic;
	public static int[] team1;
	public static int[] team2;
	public int[] playerNumbers = {1, 2, 3, 4};
	List<int> availablePlayerNums = new List<int>();
	List<int> team_1 = new List<int>();
	List<int> team_2 = new List<int>();
	string displayString;
	public float timeToNextRound;
	float timePassed = 0;

	// Use this for initialization
	void Start () {
		// Set playerNums ArrayList
		foreach (int number in playerNumbers)
			availablePlayerNums.Add(number);

		// Set Round Number Text
		GameObject.FindGameObjectWithTag ("Round").GetComponent<Text> ().text = "Round " + roundNumber;

		// Select random GameType and set GameType text
		int g = Random.Range (0, gameTypes.Length);
		gameType = gameTypes [g];
		GameObject.FindGameObjectWithTag ("GameType").GetComponent<Text> ().text = gameType;

		// Set team dynamic based on game type
		if (gameType == "Deathmatch")
				teamDynamic = "FFA";
		else if (gameType == "Team Deathmatch")
				teamDynamic = "2v2";
		else if (gameType == "King of the Hill") {
				int randInt = Random.Range (0, 2);
				Debug.Log (randInt);
				teamDynamic = possibleTeamDynamics [randInt];
		} else if (gameType == "Blowdown") {
				teamDynamic = "FFA";	
		}

		// If team dynamic is free-for-all, display FFA text
		if (teamDynamic == "FFA")
						GameObject.FindGameObjectWithTag ("Teams").GetComponent<Text> ().text = "P1 vs P2 vs P3 vs P4";
		// If team dynamic is 2v2, make teams and display them
		else if (teamDynamic == "2v2") {
				// Set up 2 vs 2 teams
				// Pick 2 random player numbers for team 1
				while (team_1.Count < 2) {
						int p = Random.Range (1, availablePlayerNums.Count);
						team_1.Add (availablePlayerNums [p - 1]);
						availablePlayerNums.Remove (availablePlayerNums [p - 1]);
				}
				// Put the remaining 2 player numbers into team 2
				foreach (int number in availablePlayerNums)
						team_2.Add (number);

				// Turn team ArrayLists into arrays
				team1 = team_1.ToArray ();
				team2 = team_2.ToArray ();

				// Display Teams text
				displayString = "P" + team1 [0] + " + P" + team1 [1] + "\nvs\nP" + team2 [0] + " + P" + team2 [1];
				GameObject.FindGameObjectWithTag ("Teams").GetComponent<Text> ().text = displayString;
		} else if (teamDynamic == "1v3") {
				// Set up 1 vs 3 teams
				// Pick 1 random player numbers for team 1
				int p = Random.Range (1, availablePlayerNums.Count);
				team_1.Add (availablePlayerNums [p - 1]);
				availablePlayerNums.Remove (availablePlayerNums [p - 1]);
				// Put the remaining 2 player numbers into team 2
				foreach (int number in availablePlayerNums)
					team_2.Add (number);
				
				// Turn team ArrayLists into arrays
				team1 = team_1.ToArray ();
				team2 = team_2.ToArray ();
				
				// Display Teams text
				displayString = "P" + team1 [0] + "\nvs\nP" + team2 [0] + " + P" + team2 [1]  + " + P" + team2 [2];
				GameObject.FindGameObjectWithTag ("Teams").GetComponent<Text> ().text = displayString;
		}

	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed >= timeToNextRound)
			Application.LoadLevel(Random.Range(2, Application.levelCount - 1));
	}
	
}
