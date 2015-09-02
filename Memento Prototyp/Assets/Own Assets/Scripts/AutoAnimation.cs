using UnityEngine;
using System.Collections;

public class AutoAnimation : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<Animation>().Play("HandAnimation");
	}
}
