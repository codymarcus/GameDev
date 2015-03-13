using UnityEngine;
using System.Collections;

public class OnClickEvent : MonoBehaviour {
	public static string matchType; 
	public static int f;
	
	public void switch1(){
		Application.LoadLevel ("RoundScreen");
		matchType = "KOTH";
		f = 2; 
	}

	public void switch2(){
		Application.LoadLevel ("RoundScreen");
		matchType = "Deathmatch";
		f = 0;
	}

	public void switch3(){
		Application.LoadLevel ("RoundScreen");
		matchType = "Team KOTH";
		f = 3; 
	}
	
	public void switch4(){
		Application.LoadLevel ("RoundScreen");
		matchType = "Team Deathmatch";
		f = 1;
	}

	public void Replay(){
		Application.LoadLevel ("StartMenu");
	}

	public void Back(){
		Application.LoadLevel ("Scene0");
	}
	
}
