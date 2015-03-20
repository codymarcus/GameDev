using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Showplace : MonoBehaviour {
	
	public GameObject[] players;
	public Slider slider;
	public static int[] rank = {0,0,0,0};
	int[] scores = {0,0,0,0};
	public int currentplace;

	// Update is called once per frame
	void Start () {
	
		scores = GameManager.scores;

		int counter = 0;
		for (int i=0; i<4; i++) {
			for (int j =0;j<4;j++){
				if (scores[i] < scores[j]) {
					counter++;
				}
			}
			rank[i] = counter;
			counter = 0;
		}

		
		int stackedrank = 0;
		for (int i=0; i<4; i++) {
			for (int j=0; j<i; j++){
				if (rank[j] == rank[i]) {
					stackedrank++;
				}
			}
			rank[i] += stackedrank;
			//stackedrank = 0;
		}

		for (int i=0; i<4; i++) {
			Debug.Log(rank[i]);
		}

		for (int k = 0; k < 4; k++) {
			if (currentplace != rank[k]) {
				players[k].SetActive(false);
			} else {
				if (currentplace != 0) {
					slider.value = scores[k];
				}
			}
		}
	}


}
