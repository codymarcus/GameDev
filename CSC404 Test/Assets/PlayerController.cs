using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject spawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Death")
		{
			transform.position = spawn.transform.position;
		}
		if (other.gameObject.tag == "Money")
		{
			Destroy(other.gameObject);
		}
	}
}
