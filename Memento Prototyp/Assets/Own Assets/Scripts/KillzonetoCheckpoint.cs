using UnityEngine;

using System.Collections;

public class KillzonetoCheckpoint : MonoBehaviour {
	public Vector3 respawnPosition;
	public GameObject KillFader;
	public AudioClip impact;
	public bool firstKillZone = false;
	AudioSource audio;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			Kill (other.gameObject);
		}
	}

	public void Kill(GameObject other){
		if (firstKillZone) {
			KillFader.SetActive (true);
			globalVariables.player.GetComponent<MultipleAudioScript> ().PLaySoundByIndex (0);
			other.transform.position = respawnPosition;
			StartCoroutine ("DeleteAfterTime");
			globalVariables.player.GetComponent<healthControle> ().KillMaya ();
		} else {
			FirstKillZone(other);
		}
	}

	IEnumerator DeleteAfterTime(){
		yield return new WaitForSeconds(3f);
		KillFader.SetActive(false);
	}

	public void FirstKillZone(GameObject other){
		KillFader.SetActive (true);
		globalVariables.player.GetComponent<MultipleAudioScript> ().PLaySoundByIndex (0);
		other.transform.position = respawnPosition;
		StartCoroutine ("DeleteAfterTime");
	}
}
