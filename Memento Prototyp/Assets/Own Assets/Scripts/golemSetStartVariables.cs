using UnityEngine;
using System.Collections;

public class golemSetStartVariables : MonoBehaviour {

	// Boss Fight

	void Start () {
		globalVariables.player = GameObject.FindGameObjectWithTag("Player");
		globalVariables.golemHand = GameObject.FindGameObjectWithTag("GolemHand");
		globalVariables.golemHand.SetActive(false);
		globalVariables.quickTRight = GameObject.FindGameObjectWithTag("QT Right");
		globalVariables.quickTRight.SetActive(false);
		globalVariables.quickTLeft = GameObject.FindGameObjectWithTag("QT Left");
		globalVariables.quickTLeft.SetActive(false);
		globalVariables.climbButtonUI = GameObject.FindGameObjectWithTag("climbButtonUI");
		globalVariables.climbButtonUI.SetActive(false);
		globalVariables.helperTextUI = GameObject.FindGameObjectWithTag("helperTextUI");
		globalVariables.helperTextUI.SetActive(false);
		globalVariables.golem = GameObject.FindGameObjectWithTag("golem");
		globalVariables.golemAnimator = globalVariables.golem.GetComponent<Animator>();
	}
}
