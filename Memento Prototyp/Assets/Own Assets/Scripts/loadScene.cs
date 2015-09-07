using UnityEngine;
using System.Collections;

public class loadScene : MonoBehaviour {
	public GameObject KillFader;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			StartCoroutine("ActivateAfterTime");
			KillFader.SetActive(true);
		}
	}

	IEnumerator ActivateAfterTime(){
		yield return new WaitForSeconds (0.8f);
		Application.LoadLevel ("_Boss_Scene");
	}
}
