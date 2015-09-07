using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClimbManagerScript : MonoBehaviour {

	public static bool climb = false; 
	public static bool showUIText = false;
	public static bool once = false;
	public static bool golemHandActive = false;
	private static Vector3 positionRightGolemHand = new Vector3(-9f, 12f, 0f);
	private static Vector3 positionLeftGolemHand = new Vector3(9f, 12f, 0f);
	private static Quaternion tempQuart;
	public static GameObject[] stages;

	void Awake(){
		stages = new GameObject[3];
		stages[0] = GameObject.Find("Stage1");
		stages[1] = GameObject.Find("Stage2");
		stages[2] = GameObject.Find("Stage3");
	}

	private static int platformcount = 0;

	public static void checkIfClimbStarts(){
		if(Input.GetKeyUp(KeyCode.C)){
			ClimbOrNot();
		}
	}

	public static void ClimbOrNot(){
		if(!climb){
			// Climb
			print ("working");
			// Set Player Gravity To 0
			globalVariables.player.GetComponent<Rigidbody2D>().gravityScale = 0f;
			//globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardControl>().enabled = false;
			globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardClimb>().enabled = true;
			globalVariables.player.GetComponent<Animator>().SetBool("Climb", true);
			globalVariables.presentPlatform.transform.Find("NotMoveCollider").gameObject.SetActive(true);
			// Set Parent for the grip
			globalVariables.player.transform.parent = globalVariables.presentPlatform.transform;

			// Set The Ui Button
			globalVariables.climbButtonUI.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
			globalVariables.climbButtonUI.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;

			// Golem Walk
			globalVariables.golemAnimator.SetBool("walk", true);
			climb = true;
		}
		else {
			// Climb
			print ("End Climb");
			DeactivateClimb();
			globalVariables.presentPlatform.transform.Find("NotMoveCollider").gameObject.SetActive(false);
			if(globalVariables.presentPlatform != null){
				globalVariables.presentPlatform.transform.Find("NotMoveCollider").gameObject.SetActive(false);
			}
			// Set The Ui Button
			globalVariables.climbButtonUI.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
			globalVariables.climbButtonUI.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;

			// Golem Not-Walk
			globalVariables.golemAnimator.SetBool("walk", false);
			// set to 0
			platformcount = 0;
		}
	}

	public void ClimbNonStatic(){
		if(!climb){
			// Climb
			print ("working");
			// Set Player Gravity To 0
			globalVariables.player.GetComponent<Rigidbody2D>().gravityScale = 0f;
			//globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardControl>().enabled = false;
			globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardClimb>().enabled = true;
			globalVariables.player.GetComponent<Animator>().SetBool("Climb", true);
			globalVariables.presentPlatform.transform.Find("NotMoveCollider").gameObject.SetActive(true);
			// Set Parent for the grip
			globalVariables.player.transform.parent = globalVariables.presentPlatform.transform.parent;
			// Set The Ui Button
			globalVariables.climbButtonUI.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
			globalVariables.climbButtonUI.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;
			// Golem Walk
			globalVariables.golemAnimator.SetBool("walk", true);
			climb = true;
		}
		else {
			// Climb
			// Activate All ClimbEvents
			for (int i = 0; i < stages.Length; i++){
				stages[i].transform.Find("Event").gameObject.SetActive(true);
			}
			print ("End Climb");
			DeactivateClimb();
			globalVariables.presentPlatform.transform.Find("NotMoveCollider").gameObject.SetActive(false);
			if(globalVariables.presentPlatform != null){
				globalVariables.presentPlatform.transform.Find("NotMoveCollider").gameObject.SetActive(false);
			}
			// Set The Ui Button
			globalVariables.climbButtonUI.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
			globalVariables.climbButtonUI.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;
			once = false;
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
		// erste mal aufgerufen
		if(!showUIText){
			showUIText = true;
			globalVariables.helperTextUI.SetActive(true);
			return;
		}
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

	public void SpawnGolemHandFirst(){
		if(!once){
			SpawnGolemHand(false);
			once = true;
		}
		else{
			globalVariables.climbButtonUI.SetActive(false);
		}
	}

	public static void DeactivateClimb(){
		// Set Player Gravity To 0
		globalVariables.player.GetComponent<Rigidbody2D>().gravityScale = 1f;
		//globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardControl>().enabled = true;
		globalVariables.player.GetComponent<UnityStandardAssets._2D.KeyboardClimb>().enabled = false;
		globalVariables.player.GetComponent<Animator>().SetBool ("Climb", false);
		climb = false;
		globalVariables.player.transform.parent = GameObject.FindGameObjectWithTag("characterHolder").transform;
		// Set player Rotation to 0
		tempQuart = globalVariables.player.transform.rotation;
		globalVariables.player.transform.rotation = new Quaternion(tempQuart.x, tempQuart.y, 0f, tempQuart.w);
		platformcount = 0;
	}
	
	// Call in QuickTimeBubblemanager
	public static void JumpToNextPlatfrom(){
		//globalVariables.presentPlatform.transform.Find("NotMoveCollider").gameObject.SetActive(false);
		//globalVariables.presentPlatform.transform.Find("Event").gameObject.SetActive(false);
		stages[platformcount].transform.Find("NotMoveCollider").gameObject.SetActive(false);
		stages[platformcount].transform.Find("Event").gameObject.SetActive(false);
		globalVariables.player.transform.position = globalVariables.nextPlatformBottom.transform.position;
		globalVariables.nextPlatformBottom.transform.parent.Find("NotMoveCollider").gameObject.SetActive(true);
		globalVariables.player.transform.parent = globalVariables.nextPlatformBottom.transform.parent.parent;
		if(globalVariables.lastPlatfrom){
			ClimbManagerScript.DeactivateClimb();
			globalVariables.lastPlatfrom = false;
			globalVariables.isZoom = true;
			globalVariables.climbButtonUI.SetActive(false);
			// after Zoom show Helper UI (in ZoomIn)
		}
		platformcount++;
		SwitchPlatform();
		globalVariables.player.transform.rotation = Quaternion.identity;
	}

	static void SwitchPlatform(){
		switch(platformcount){
			case 1:
				globalVariables.golemAnimator.SetBool("walk", false);
				globalVariables.golemAnimator.SetBool("punch2", true);
				break;
			case 2:
				globalVariables.golemAnimator.SetBool("punch2", false);
				globalVariables.golemAnimator.SetBool("walk", true);
				break;
			case 3:
				break;
		 	default:
				break;
		}
	}

	public void startWaitforSec(){
		StartCoroutine("setGolemHandActive");
	}
	
	IEnumerator setGolemHandActive(){
		yield return new WaitForSeconds(1f);
		ClimbManagerScript.golemHandActive = false;
	}
}
