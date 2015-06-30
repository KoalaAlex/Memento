using UnityEngine;
using System.Collections;

public class enablePowerAura : MonoBehaviour {
	public string childName = "PowerAura";
	private GameObject stunnedParticle;

	// Use this for initialization
	void Start () {
		stunnedParticle = gameObject.transform.FindChild(childName).gameObject;
	}
	
	// Update is called once per frame
	void StunnedActivate() {
		stunnedParticle.SetActive(true);
	}

	void StunnedDeactivate() {
		stunnedParticle.SetActive(false);
	}
}
