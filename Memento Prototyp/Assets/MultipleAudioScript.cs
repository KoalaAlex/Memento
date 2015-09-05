using UnityEngine;
using System.Collections;

public class MultipleAudioScript : MonoBehaviour {

	public AudioClip[] audioClips;
	public int index = 0;

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
		StartCoroutine ("ChangeSound");
	}

	public void PLaySoundByIndex(int _index){
		audioSource.clip = audioClips [_index];
		audioSource.Stop ();
		audioSource.Play ();
	}
}
