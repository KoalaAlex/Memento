using UnityEngine;
using System.Collections;

public class EnemyAttackTriggerScript : MonoBehaviour {
	public GameObject enemy;
	private checkCollisionWithPlayer collisionScript;

	void Start(){
		collisionScript = enemy.GetComponent<checkCollisionWithPlayer>();
	}

	void OnTriggerEnter2D(){
		collisionScript.attackIntersectsPlayer = true;
	}

	void OnTriggerStay2D(){
		collisionScript.attackIntersectsPlayer = true;
	}

	void OnTriggerExit2D(){
		collisionScript.attackIntersectsPlayer = false;
	}
}
