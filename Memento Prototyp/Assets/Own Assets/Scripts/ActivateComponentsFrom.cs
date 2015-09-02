using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ActivateComponentsFrom : MonoBehaviour {
	public GameObject[] goEnemys;
	public string nameComponent = "KeyboardControl";

	/* Platformer2DUserControl / KeyboardControl */
	public void DeactivateActions(){
		globalVariables.player.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = false;
		globalVariables.levi.GetComponent<MoveMultiTouch>().enabled = false;
		for (int i = 0; i < goEnemys.Length; i++){
			goEnemys[i].GetComponent<ICode.ICodeTrigger2D>().enabled = false;
		}
	}

	IEnumerator ActivateActions(){
		yield return new WaitForSeconds(1);
		print ("DIalog ENDE!");
		globalVariables.player.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = true;
		globalVariables.levi.GetComponent<MoveMultiTouch>().enabled = true;
		for (int i = 0; i < goEnemys.Length; i++){
			goEnemys[i].GetComponent<ICode.ICodeTrigger2D>().enabled = true;
		}
	}

	public void ActivateActionCoroutine(){
		StartCoroutine(ActivateActions());
	}
}
