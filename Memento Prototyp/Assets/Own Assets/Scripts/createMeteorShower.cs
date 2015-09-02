using UnityEngine;
using System.Collections;

public class createMeteorShower : MonoBehaviour {

	public Transform meteor;

	private Transform myMeteor;
	private Vector3 position;

	void Start(){
		StartCoroutine("nextMeteor");
	}

	IEnumerator nextMeteor(){
		position = new Vector3(Random.Range(-4f, 4f), 0f, 0f);
		// create Meteor from Prefab
		myMeteor = Instantiate(meteor, position, Quaternion.identity) as Transform;;
		myMeteor.parent = transform;
		yield return new WaitForSeconds(2);
		StartCoroutine("nextMeteor");
	}
}
