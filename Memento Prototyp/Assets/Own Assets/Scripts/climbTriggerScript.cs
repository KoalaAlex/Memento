using UnityEngine;
using System.Collections;

public class climbTriggerScript : MonoBehaviour {
	private bool callOnlyOnce = false;

	void OnTriggerEnter2D(Collider2D other){
		print ("Ready To Climb!!!");
		globalVariables.presentPlatform = gameObject.transform.parent.gameObject;
		globalVariables.climbButtonUI.SetActive(true);
	}

	void OnTriggerStay2D(Collider2D other){
		if(!callOnlyOnce){
			ClimbManagerScript.checkIfClimbStarts();
			callOnlyOnce = true;
		}
	}

	void OnTriggerExit(){
		globalVariables.climbButtonUI.SetActive(false);
	}

	void LateUpdate(){
		callOnlyOnce = false;
	}
}
