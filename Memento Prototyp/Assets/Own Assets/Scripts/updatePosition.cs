using UnityEngine;
using System.Collections;

public class updatePosition : MonoBehaviour {
	public GameObject target;

	private Vector3 pos;
	// Use this for initialization
	void Start () {
		pos = target.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		pos = target.transform.position;
		gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y,  gameObject.transform.position.z);
	}
}
