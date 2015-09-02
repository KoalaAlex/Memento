using UnityEngine;
using System.Collections;

public class climbTopCollider : MonoBehaviour {
	public GameObject nextPlatformBottom;
	public bool jumpToRight = false;
	public bool lastPlatform = false;

	void OnTriggerEnter2D(Collider2D other){
		ClimbManagerScript.SetTopValue(true);
		ClimbManagerScript.SpawnGolemHand(jumpToRight);
		globalVariables.nextPlatformBottom = nextPlatformBottom;
		if(lastPlatform){
			globalVariables.lastPlatfrom = lastPlatform;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		ClimbManagerScript.SetTopValue(false);
	}
}
