using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	public static GameControl control;

	public GameObject dialog;
	public GameObject noDialog;
	// Use this for initialization
	void Awake(){
		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control != this){
			Destroy(gameObject);
		}

	}

	void Start () {
		noDialog.SetActive(false);
		// Set once Dialog enabled
		dialog.SetActive(true);
	}
}
