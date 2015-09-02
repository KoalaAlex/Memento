using UnityEngine;
using System.Collections;

public class resetAniationShuriken : MonoBehaviour {

	public float resetAfter = 2f;

	// Use this for initialization
	void Start () {
		StartCoroutine("resetParticleSystem");
	}

	IEnumerator resetParticleSystem(){
		yield return new WaitForSeconds(resetAfter);
		gameObject.GetComponent<ParticleSystem>().Stop();
		gameObject.GetComponent<ParticleSystem>().Play();
		StartCoroutine("resetParticleSystem");
	}
}
