using UnityEngine;
using System.Collections;

public class QuickTimeBubbleManagerScript : MonoBehaviour {
	public bool allBubbles = false;
	public bool start = false;
	private QuickTimeBubbleScript childrenBubbleScript;

	public void startTheRound(){
		for(int i= 0; i < transform.childCount; i++){
			childrenBubbleScript = transform.GetChild(i).GetComponent<QuickTimeBubbleScript>();
			childrenBubbleScript.swiped = false;
			childrenBubbleScript.resetBubbleColor();
		}
		start = true;
	}

	public void checkIfAllBubblesAreTriggered(){
		allBubbles = true;
		for(int i=0; i < transform.childCount; i++){
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
		}
		start = false;
		gameObject.SetActive(false);
	}
}
