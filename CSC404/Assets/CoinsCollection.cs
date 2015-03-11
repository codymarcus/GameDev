using UnityEngine;
using System.Collections;

public class CoinsCollection : MonoBehaviour {

	public GameObject[] coins;
	int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < coins.Length; i++)
		{
			if(coins[i] == null)
				count++;
		}
		if (coins.Length == count)
			Destroy (this.gameObject);
	}
}
