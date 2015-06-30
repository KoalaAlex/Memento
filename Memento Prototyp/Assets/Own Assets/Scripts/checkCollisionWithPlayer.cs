using UnityEngine;
using System.Collections;
using ICode;

public class checkCollisionWithPlayer : MonoBehaviour {
	private GameObject player;
	public GameObject enemyDmgRadius;
	private ICodeBehaviour behavior;
	public float shieldArea = 180;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		enemyDmgRadius = GameObject.FindGameObjectWithTag("EnemyDmgRadius");
		behavior = gameObject.GetBehaviour();
	}

	void CheckIfEnemyAttackIntersectsWithPlayer(float radius){
		if(Vector3.Distance(player.transform.position, enemyDmgRadius.transform.position) < radius){
			behavior.stateMachine.SetVariable("doDamage", true);
		}
		print (Vector3.Distance(player.transform.position, enemyDmgRadius.transform.position));
	}
}
