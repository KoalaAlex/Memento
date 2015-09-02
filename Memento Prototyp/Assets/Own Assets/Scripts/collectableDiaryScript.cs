using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class collectableDiaryScript : MonoBehaviour {
	public Transform collectParticle;

	void OnTriggerEnter2D(){
		IncreaseTheNumberOfDiarySites();
		Instantiate(collectParticle, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}

	void IncreaseTheNumberOfDiarySites(){
		globalVariables.collectedDiarySites++;
		globalVariables.diarySitesUI.GetComponent<Text>().text = globalVariables.collectedDiarySites + "/1";
	}
}
