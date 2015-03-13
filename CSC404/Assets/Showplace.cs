using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Showplace : MonoBehaviour {
	
	public GameObject[] players;
	public Slider slider;
	public static int[] place = {0,0,0,0};
	public static int[] sortedscores = {0,0,0,0};
	int[] scores = {0,0,0,0};
	public int currentplace;

	// Update is called once per frame
	void Start () {
	
		//scores = ScoreScreenManager.matchScores;
		//sortedscores = ScoreScreenManager.matchScores;
		sortedscores = Sort (sortedscores);

		for(int i = 0; i < 4; i++){
			for(int j = 0; j < 4; j++){
				if ((sortedscores[i] == scores[j]) && (ScoreScreenManager.rank[j] == 0)){
					place[i] = j;
					ScoreScreenManager.rank[j] = -1;
					break;
				}
			}
		}

		for (int k = 0; k < 4; k++) {
			if (k != place[currentplace]) {
				players[k].SetActive(false);
			} 
		}

		if (currentplace != 0) {
			slider.value = sortedscores[currentplace];
		}


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
