using UnityEngine;
using System.Collections;

public class climbTriggerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		print ("Ready To Climb!!!");
	}

	void OnTriggerStay2D(Collider2D other){
		ClimbManagerScript.checkIfClimbStarts();
	}
}
