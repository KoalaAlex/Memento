using UnityEngine;
using System.Collections;

public class LeviLookPosition : MonoBehaviour {

	private float posDiv = 0f;
	private float prevRotation = 0f;
	private bool rightSide = false;
	private bool prevRightSide = false;

	private Vector3 localScaleVar;

	// Rotate Levi
	void FixedUpdate () {
		posDiv = transform.position.x - globalVariables.player.transform.position.x;
		if(posDiv > 0){
			rightSide = true;
		}
		else{
			rightSide = false;
		}
		if(rightSide != prevRightSide){
			FlipInX();
			prevRightSide = rightSide;
		}
	}

	void FlipInX(){
		localScaleVar = transform.localScale;
		localScaleVar.x = localScaleVar.x * -1;
		transform.localScale = localScaleVar;
	}
}
