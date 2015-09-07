using UnityEngine;
using System.Collections;
using ICode;

public class LightTriggerScript : MonoBehaviour {
	public GameObject prefabDeath;
	public GameObject plasma;
	[Range(3,7)]
	public float fillSpeed = 5f;
	[Range(0.02f,0.06f)]
	public float negativFillSpeed = 0.04f;
	private int count = 0;
	private ICodeBehaviour behaviour;
	private Vector3 pos;
	private float percent;
	private float maxFillRate = 10f;
	private bool destroy = false;
	private bool once = false;
	private GameObject parent;
	private ParticleSystem plasmaPS;

	void Start(){
		behaviour = gameObject.transform.parent.gameObject.GetBehaviour();
		percent = 0f;
		plasmaPS = plasma.GetComponent<ParticleSystem> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		plasma.SetActive (true);
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
			percent += fillSpeed * Time.deltaTime;
			print (percent);
			if(!destroy && (percent >= maxFillRate)){
				destroy = true;
			}
			plasmaPS.startColor = new Color(255, 255, 255, percent / maxFillRate);
		}
	}

	void FixedUpdate(){
		if (percent > 0f) {
			percent -= negativFillSpeed;
			plasmaPS.startColor = new Color(255, 255, 255, percent / maxFillRate);
		}
		if(destroy && !once){
			parent = transform.parent.gameObject;
			parent.GetComponent<MeshRenderer>().enabled = false;
			prefabDeath.SetActive(true);
			plasma.SetActive(false);
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			Destroy(transform.parent.gameObject, 3f);
			once = true;
		}
	}
}
