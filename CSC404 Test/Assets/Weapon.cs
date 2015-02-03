using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public int playerNumber;
	public GameObject bullet;
	public GameObject barrel;
	public PlayerController player;
	float canFire = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButton(playerNumber + "Fire1") && canFire <= 0 && player.UseAmmo()) 
		{ 
			GameObject b = (GameObject) Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
			Destroy( b , 4);
			canFire = .2f;
		}
		if (canFire <= 0)
			canFire = 0;
		else
			canFire -= Time.deltaTime;
	}
}
