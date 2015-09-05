using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class playAudio : MonoBehaviour {
	public AudioClip impact;
	public GameObject fadePanel;

	private string changeLevelName = "_Tutorial Level";

	private Image img;

	void Start(){
		img = fadePanel.GetComponent<Image> ();
	}

	AudioSource audio;
	
	public void playSound() {
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(impact, 0.7F);
	}
	
	public void playFadeIn(){
		fadePanel.SetActive (true);
		StartCoroutine ("FadeIn");
	}

	IEnumerator FadeIn(){
		img.color = new Color (img.color.r, img.color.g, img.color.b, img.color.a + 0.05f);
		yield return new WaitForSeconds (0.02f);
		if (img.color.a >= 1) {
			Application.LoadLevel(changeLevelName);
		} else {
			StartCoroutine ("FadeIn");
		}
	}
}