using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ActivateComponentsFrom : MonoBehaviour {
	public GameObject goPlayer;
	public GameObject goEnemy;
	public string nameComponent = "Platformer2DUserControl";

	// Use this for initialization
	void Awake () {
		goPlayer = GameObject.FindGameObjectWithTag("Player");
	} 

	/* Platformer2DUserControl / KeyboardControl */
	void DeactivateActions(){
		goPlayer.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = false;
		goEnemy.GetComponent<ICode.ICodeTrigger2D>().enabled = false;
	}

	IEnumerator ActivateActions(){
		yield return new WaitForSeconds(1);
		goPlayer.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = true;
		goEnemy.GetComponent<ICode.ICodeTrigger2D>().enabled = true;
	}
}
