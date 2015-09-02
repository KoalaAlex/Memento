using UnityEngine;
using System.Collections;

public class ClimbManagerScript : MonoBehaviour {

	private static bool climb = false;
	private static Vector3 positionRightGolemHand = new Vector3(-6f, 9f, 0f);
	private static Vector3 positionLeftGolemHand = new Vector3(6f, 9f, 0f);

	public static void checkIfClimbStarts(){
		if(Input.GetKeyDown(KeyCode.C)){
			if(!climb){
				// Climb
				print ("working");
				// Set Player Gravity To 0
				globalVariables.player.GetComponent<Rigidbody2D>().gravityScale = 0f;
				globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardControl>().enabled = false;
				globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardClimb>().enabled = true;
				climb = true;
			}
			else {
				// Climb
				print ("End Climb");
				DeactivateClimb();
			}
		}
	}

	public static void SetBottomValue(bool value){
		globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardClimb>().bottom = value;
	}

	public static void SetTopValue(bool value){
		globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardClimb>().top = value;
	}

	// Call in ClimbTopCollider
	public static void SpawnGolemHand(bool spawnTopRight){
		globalVariables.golemHand.SetActive(true);
		if(spawnTopRight){
			globalVariables.golemHand.GetComponent<golemHandAttack>().setDirection(spawnTopRight);
			globalVariables.golemHand.transform.position = globalVariables.player.transform.position + positionRightGolemHand;
			globalVariables.quickTRight.SetActive(true);
			globalVariables.quickTRight.GetComponent<QuickTimeBubbleManagerScript>().startTheRound();
		}
		else{
			globalVariables.golemHand.GetComponent<golemHandAttack>().setDirection(spawnTopRight);
			globalVariables.golemHand.transform.position = globalVariables.player.transform.position + positionLeftGolemHand;
			globalVariables.quickTLeft.SetActive(true);
			globalVariables.quickTLeft.GetComponent<QuickTimeBubbleManagerScript>().startTheRound();
		}
	}

	public static void DeactivateClimb(){
		// Set Player Gravity To 0
		globalVariables.player.GetComponent<Rigidbody2D>().gravityScale = 1f;
		globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardControl>().enabled = true;
		globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardClimb>().enabled = false;
		climb = false;
	}
	
	// Call in QuickTimeBubblemanager
	public static void JumpToNextPlatfrom(){
		globalVariables.player.transform.position = globalVariables.nextPlatformBottom.transform.position;
		if(globalVariables.lastPlatfrom){
			ClimbManagerScript.DeactivateClimb();
			globalVariables.lastPlatfrom = false;
		}
	}
}
