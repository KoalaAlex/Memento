using UnityEngine;
using System.Collections;

public class ActivateAtStart : MonoBehaviour {
	public GameObject go;

	// Use this for initialization
	void Start () {
		go.SendMessage("ActivateActions");
	}

}
