#pragma strict

/* Written by Cassius Adams for the Super Space Trooper video game. http://www.SuperSpaceTrooper.com */
/*
This script makes the engine glow pulsate.  It should be used on a transparent
material with alpha channel.  Change the 'growSize' variable to make the engine
glow grow proportionally in scale
*/
public var startTime : float = 0; 		// Time that the resize begins at
public var minGlowSize : Vector3; 		// The minimum size of the engine glow
public var maxGlowSize : Vector3; 		// The maximum size of the engine glow
public var player : Transform; 		// The Player gameObject's transform
public var growSize : float = 1.2; 	// The size the engine glow grows to become
public var duration : float = 0.15; 	// The amount of time the engine glow takes to shrink and then grow
 
function Start() {
	player = GameObject.Find("PlayerObjectName").transform; // Find the Player's transform and assign it
	minGlowSize = transform.localScale; // Get the current size of the engine glow (this should be the small size)
	maxGlowSize = minGlowSize*growSize; // Assign the maximum size of the engine glow
	startTime = Time.time; // Keep track of time so we can change the duration
	duration = duration/2; // So that it's in cycles per second
}
 
function LateUpdate() {
	if(player) {
		var timer = (Time.time - startTime) / duration;
		transform.localScale = Vector3.Lerp(minGlowSize, maxGlowSize, timer); //
		if(timer >= 1) {
	    	startTime = Time.time;
	    	/* Now that it's the maximum size, reverse the order so it shrinks again */
	    	var tempMinGlowSize = maxGlowSize;
	    	maxGlowSize = minGlowSize;
	    	minGlowSize = tempMinGlowSize;
    	}
	}
}