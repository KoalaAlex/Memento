using UnityEngine;
using System.Collections;

public class firstEnemyEncounter : MonoBehaviour {
	public Transform endMovePoint;
	public Transform deadParticle;

	private moveEnemySpine MoveScript;
	private Animator anim;
	private bool pointReached = false;

	// Use this for initialization
	void Awake () {
		MoveScript = gameObject.GetComponent<moveEnemySpine>();
		anim = gameObject.GetComponent<Animator>();
	}

	void Update(){
		if(transform.position.x > endMovePoint.position.x){
			MoveScript.Move();
			anim.SetBool("Run", true);
		}
		else{
			MoveScript.Stop();
			anim.SetBool("Run", false);
			if(!pointReached){
				globalVariables.controlActivator.GetComponent<loadPrefabDialog>().loadSecondDialog();
				pointReached = true;
			}
		}
	}

	public void DeadAnimation(){
		Instantiate(deadParticle, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
