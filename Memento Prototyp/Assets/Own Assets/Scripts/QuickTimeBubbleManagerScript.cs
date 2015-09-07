using UnityEngine;
using System.Collections;

public class QuickTimeBubbleManagerScript : MonoBehaviour {
	public bool allBubbles = false;
	public bool start = false;
	public GameObject climbManagerScript;
	private QuickTimeBubbleScript childrenBubbleScript;
	private Vector3 tempPos;

	public void startTheRound(){
		for(int i= 1; i < transform.childCount; i++){
			childrenBubbleScript = transform.GetChild(i).GetComponent<QuickTimeBubbleScript>();
			childrenBubbleScript.swiped = false;
			childrenBubbleScript.resetBubbleColor();
		}
		start = true;
	}

	public void checkIfAllBubblesAreTriggered(){
		allBubbles = true;
		for(int i=1; i < transform.childCount; i++){
			if(transform.GetChild(i).GetComponent<QuickTimeBubbleScript>().swiped == false){
				allBubbles = false;
				break;
			}
		}
		if(allBubbles){
			print ("All Bubbles complete");
			// Jump To Next Platform -ClimbManager-
			ClimbManagerScript.JumpToNextPlatfrom();
		}
		else{
			print ("Fail!");
			globalVariables.golemHand.GetComponent<golemHandAttack>().speed = 4f;
			globalVariables.player.GetComponent<healthControle>().TakeDamage(30);
		}
		start = false;
		gameObject.SetActive(false);
		// Start eraly after Seconds
		climbManagerScript.GetComponent<ClimbManagerScript>().startWaitforSec();
	}
}
