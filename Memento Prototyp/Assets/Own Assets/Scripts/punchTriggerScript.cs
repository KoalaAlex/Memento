using UnityEngine;
using System.Collections;

public class punchTriggerScript : MonoBehaviour {

	void OnTriggerEnter2D(){
		globalVariables.golemAnimator.SetBool("punch1", true);
	}

	void OnTriggerExit2D(){
		globalVariables.golemAnimator.SetBool("punch1", false);
	}
}
