using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject bullet;
	public GameObject barrel;
	float canFire = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButton("Fire1") && canFire == 0) 
		{ 
			GameObject b = (GameObject) Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
			Destroy( b , 4);
			canFire = .05f;
		}
		if (canFire <= 0)
			canFire = 0;
		else
			canFire -= Time.deltaTime;
	}
}
