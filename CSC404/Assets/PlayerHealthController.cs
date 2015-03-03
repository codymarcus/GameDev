using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthController : MonoBehaviour
{
	public int startingHealth = 3;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public GameObject Health1;                                 // Reference to the UI's health
	public GameObject Health2;   
	public GameObject Health3;   
	
	void Awake ()
	{
		currentHealth = startingHealth;
	}

	void Update ()
	{
		if (currentHealth <= 2) {
			Health3.gameObject.renderer.enabled = false;
		}
		if (currentHealth <= 1) {
			Health2.gameObject.renderer.enabled = false;
		}
		if (currentHealth <= 0) {
			Health1.gameObject.renderer.enabled = false;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		// If player touches a death object then die
		if (other.gameObject.tag == "Death" || other.gameObject.tag == "Enemy")
		{
			currentHealth -= 1;
		}
	}	    
}