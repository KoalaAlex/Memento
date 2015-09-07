using UnityEngine;
using System.Collections;

public class deleteAfterTime : MonoBehaviour {
	public float deleteTime = 2f;

	// Use this for initialization
	void Awake () {
		StartCoroutine("DeleteAfterTime");
	}
	
	IEnumerator DeleteAfterTime(){
		yield return new WaitForSeconds(deleteTime);
		Destroy(this.gameObject);
	}
}
