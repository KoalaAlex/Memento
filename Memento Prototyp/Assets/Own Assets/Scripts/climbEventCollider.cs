using UnityEngine;
using System.Collections;

public class climbEventCollider : MonoBehaviour {
	public GameObject nextPlatformBottom;
	public bool jumpToRight = false;
	public bool lastPlatform = false;

	private bool onlyOncePerFrame = true;

	void OnTriggerEnter2D(Collider2D other){
		if(onlyOncePerFrame && !ClimbManagerScript.golemHandActive && ClimbManagerScript.climb){
			onlyOncePerFrame = false;

			ClimbManagerScript.SetTopValue(true);
			if(ClimbManagerScript.climb){
				ClimbManagerScript.golemHandActive = true;
				ClimbManagerScript.SpawnGolemHand(jumpToRight);
				print ("SpawnGilem Hand");
			}
			globalVariables.nextPlatformBottom = nextPlatformBottom;
			if(lastPlatform){
				globalVariables.lastPlatfrom = lastPlatform;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		ClimbManagerScript.SetTopValue(false);
	}

	void LateUpdate(){
		onlyOncePerFrame = true;
	}
}
