using UnityEngine;
using System.Collections;
using ICode;

public class checkCollisionWithPlayer : MonoBehaviour {
	public GameObject enemyDmgRadius;
	public Collider2D enemyDamageCollider;
	public bool attackIntersectsPlayer = false;

	private ICodeBehaviour behavior;

	void Start(){
		behavior = gameObject.GetBehaviour();
	}

	public void CheckIfEnemyAttackIntersectsWithPlayer(){
		if(attackIntersectsPlayer){
			behavior.stateMachine.SetVariable("doDamage", true);
		}
		print ("Script is working");
	}
}
