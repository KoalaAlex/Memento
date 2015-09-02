using UnityEngine;
using System.Collections;
using ICode;

public class LightTriggerScript : MonoBehaviour {
	private int count = 0;
	private ICodeBehaviour behaviour;
	private Vector3 pos;

	void Start(){
		behaviour = gameObject.transform.parent.gameObject.GetBehaviour();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Light")
		{
			behaviour.SendEvent("Stunned", "");
		}
		else if(other.tag == "JumpTrigger"){
			pos = other.transform.GetChild(0).position;
			behaviour.SendEvent("Jump", pos);
		}
		else if(other.tag == "StopTrigger"){
			behaviour.SendEvent("Stop", "");
			print ("STOP!!!");
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Light")
		{
			behaviour.SendEvent("Stunned", "");
		}
	}
}
