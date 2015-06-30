using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ActivateComponentsFrom : MonoBehaviour {
	private GameObject goPlayer;
	public GameObject[] goEnemys;
	public string nameComponent = "Platformer2DUserControl";

	// Use this for initialization
	void Awake () {
		goPlayer = GameObject.FindGameObjectWithTag("Player");
	} 

	/* Platformer2DUserControl / KeyboardControl */
	void DeactivateActions(){
		goPlayer.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = false;
		for (int i = 0; i < goEnemys.Length; i++){
			goEnemys[i].GetComponent<ICode.ICodeTrigger2D>().enabled = false;
		}
	}

	IEnumerator ActivateActions(){
		yield return new WaitForSeconds(1);
		print ("DIalog ENDE!");
		goPlayer.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = true;
		for (int i = 0; i < goEnemys.Length; i++){
			goEnemys[i].GetComponent<ICode.ICodeTrigger2D>().enabled = true;
		}
	}
}
