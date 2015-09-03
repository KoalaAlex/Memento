using UnityEngine;
using System.Collections;

public class playVideo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Handheld.PlayFullScreenMovie ("MementoIntro.mp4", Color.black, FullScreenMovieControlMode.Hidden);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
