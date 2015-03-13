using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Showplace : MonoBehaviour {
	
	public GameObject[] players;
	public Slider slider;
	public static int[] place = {0,0,0,0};
	public static int[] sortedscores = {7,9,34,1};
	int[] scores = {7,9,34,1};
	public int currentplace;

	// Update is called once per frame
	void Start () {
	
		//scores = ScoreScreenManager.matchScores;
		//sortedscores = ScoreScreenManager.matchScores;
		sortedscores = Sort (sortedscores);

		for(int i = 0; i < 4; i++){
			for(int j = 0; j < 4; j++){
				if (sortedscores[i] == scores[j]){
					place[i] = j;
				} 
			}
		}

		for (int k = 0; k < 4; k++) {
			if (k != place[currentplace]) {
				players[k].SetActive(false);
			}
		}

		Debug.Log (sortedscores[0]);
		Debug.Log (sortedscores[1]);
		Debug.Log (sortedscores[2]);
		Debug.Log (sortedscores[3]);
		Debug.Log (currentplace);
		slider.value = sortedscores[currentplace];


	}

	int[] Sort (int[] list) {
		int[] result = {0,0,0,0};
		int bigger = 0;

		for (int i = 0; i < 4; i++) {
			bigger = 0;
			for (int j = 0; j < 4; j++) {
				if (list[i] > list[j]) {
					bigger++;
				}
			}
			result[3-bigger] = list[i];
		}

		return result;
	}



}
