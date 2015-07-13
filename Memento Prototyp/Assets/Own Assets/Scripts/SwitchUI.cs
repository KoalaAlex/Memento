using UnityEngine;
using System.Collections;

public class SwitchUI : MonoBehaviour {
	string canvasTag = "UIHolder";
	GameObject go0;
	GameObject go1;
	GameObject go2;

	// Use this for initialization
	void Start () {
		go0 = GameObject.FindGameObjectWithTag(canvasTag).transform.GetChild(0).gameObject;
		go1 = GameObject.FindGameObjectWithTag(canvasTag).transform.GetChild(1).gameObject;
		go2 = GameObject.FindGameObjectWithTag(canvasTag).transform.GetChild(2).gameObject;
		print (go0.name);
		print (go1.name);
		print (go2.name);
	}

	
	public void SwitchUIByNumber(int number)
	{
		switch (number){
		case 1: go0.SetActive(true); go1.SetActive(false); go2.SetActive(false);
			break;
			
		case 2: go0.SetActive(false); go1.SetActive(true); go2.SetActive(false);
			break;
			
		case 3: go0.SetActive(false); go1.SetActive(false); go2.SetActive(true);
			break;
			
		default: break;
		}
	}
}
