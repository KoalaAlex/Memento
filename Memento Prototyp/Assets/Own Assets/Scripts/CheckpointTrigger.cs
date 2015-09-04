using UnityEngine;
using System.Collections;

public class CheckpointTrigger : MonoBehaviour {

	public GameObject checkpointDialog;
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			checkpointDialog.SetActive(true);
		}

	}
}
