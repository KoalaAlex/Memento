using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class collectableSphereScript : MonoBehaviour {
	public Transform collectParticle;
	public GameObject Cutscene;
	
	void OnTriggerEnter2D(){
		IncreaseTheNumberOfDiarySites();
		Instantiate(collectParticle, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
	
	void IncreaseTheNumberOfDiarySites(){
		globalVariables.collectedSpheres++;
		globalVariables.sphereUI.GetComponent<Text>().text = "+ " + globalVariables.collectedSpheres;

		if (globalVariables.collectedSpheres == 25) {
			Cutscene.SetActive(true);
		}
	}
}
