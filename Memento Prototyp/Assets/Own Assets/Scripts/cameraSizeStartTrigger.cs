using UnityEngine;
using System.Collections;

public class cameraSizeStartTrigger : MonoBehaviour {
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D coll){
		globalVariables.nearBossEnemy = true;
	}
}
