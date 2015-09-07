using UnityEngine;
using System.Collections;

public class onlySandLevelGlobal : MonoBehaviour {
	public GameObject cutscene;
	
	void Start () {
		globalVariables.Cutscene = cutscene;
	}
}
