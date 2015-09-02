using UnityEngine;
using System.Collections;

public class golemHandAttack : MonoBehaviour {

	public Vector3 directionRight = new Vector3 (0.1f,-0.1f,0f);
	public Vector3 directionLeft = new Vector3 (-0.1f,-0.1f,0f);
	public Vector3 direction;
	public float speed = 0.5f;
	public float prevSpeed = 0.5f;

	void FixedUpdate(){
		transform.position += direction * speed;
		StartCoroutine("EndTime");
	}

	IEnumerator EndTime(){
		yield return new WaitForSeconds(2);
		speed = 4f;
		yield return new WaitForSeconds(1);
		speed = prevSpeed;
		gameObject.SetActive(false);
	}

	// Call in ClimbManager
	public void setDirection(bool right){
		if(right){
			direction = directionRight;
		}
		else{
			direction = directionLeft;
		}
	}
}
