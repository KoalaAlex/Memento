using UnityEngine;
using System.Collections;

public class enableAttackParticle : MonoBehaviour {
	public string childName = "Krunch";
	private GameObject attackParticle;
	
	// Use this for initialization
	void Start () {
		attackParticle = gameObject.transform.FindChild(childName).gameObject;
	}
	
	// Update is called once per frame
	void AttackParticleActivate() {
		attackParticle.SetActive(true);
	}
	
	void AttackParticleDeactivate() {
		attackParticle.SetActive(false);
	}
}
