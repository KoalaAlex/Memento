using UnityEngine;

using System.Collections;

public class KillzonetoCheckpoint : MonoBehaviour {
	public Vector3 respawnPosition;
	public GameObject KillFader;
	public AudioClip impact;
	AudioSource audio;

	private void OnTriggerEnter2D(Collider2D other)
	{
		KillFader.SetActive(false);
		if (other.tag == "Player") {
			KillFader.SetActive(true);
			globalVariables.player.GetComponent<MultipleAudioScript>().PLaySoundByIndex(0);
			other.transform.position = respawnPosition;
		}
	}

//	private void playKillSound(){
//		audio
//	}
}
