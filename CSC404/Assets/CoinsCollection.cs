using UnityEngine;
using System.Collections;

public class CoinsCollection : MonoBehaviour {

	public GameObject[] coins;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ArrayList check_coins = new ArrayList();
		for(int i = 0; i < coins.Length; i++)
		{
			if(coins[i] == null && !check_coins.Contains(i))
				check_coins.Add(i);
		}
		if (coins.Length == check_coins.Count) {
				Destroy (this.gameObject);
		}
	}
}
