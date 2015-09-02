using UnityEngine;
using System.Collections;

public class climbBottomScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		ClimbManagerScript.SetBottomValue(true);
	}
	
	void OnTriggerExit2D(Collider2D other){
		ClimbManagerScript.SetBottomValue(false);
	}
}
