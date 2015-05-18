using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ActivateComponentsFrom : MonoBehaviour {
	public GameObject goPlayer;
	public GameObject goEnemy;

	// Use this for initialization
	void Awake () {
		goPlayer = GameObject.FindGameObjectWithTag("Player");
	} 

	void DeactivateActions(){
		goPlayer.GetComponent<UnityStandardAssets._2D.KeyboardControl>().enabled = false;
		goEnemy.GetComponent<ICode.ICodeTrigger2D>().enabled = false;
	}

	IEnumerator ActivateActions(){
		yield return new WaitForSeconds(1);
		goPlayer.GetComponent<UnityStandardAssets._2D.KeyboardControl>().enabled = true;
		goEnemy.GetComponent<ICode.ICodeTrigger2D>().enabled = true;
	}
}
