using UnityEngine;
using System.Collections;
using ICode;

public class checkCollisionWithPlayer : MonoBehaviour {
	public bool attackIntersectsPlayer = false;

	private ICodeBehaviour behavior;

	void Start(){
		behavior = gameObject.GetBehaviour();
	}

	public void CheckIfEnemyAttackIntersectsWithPlayer(){
		if(attackIntersectsPlayer){
			behavior.stateMachine.SetVariable("doDamage", true);
			print ("NO");
		}
		print ("Script is working");
	}
}
