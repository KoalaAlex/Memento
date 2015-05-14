using UnityEngine;
using System.Collections;
using GestureRecognizer;

public class GestureRecScript : MonoBehaviour {
	public void OnGestureRecognition(Result r) {
		Debug.Log("Gesture is " + r.Name + " and scored: " + r.Score);
	}

	public void OnEnable() {
		GestureBehaviour.OnRecognition += OnGestureRecognition;
	}
	public void OnDisable() {
		GestureBehaviour.OnRecognition -= OnGestureRecognition;
	}
	public void OnDestroy() {
		GestureBehaviour.OnRecognition -= OnGestureRecognition;
	}
}