using UnityEngine;
using System.Collections;

public class SandLevelCutscene1 : MonoBehaviour {

	public Vector3 cameraPosition;
	public Camera camera;
	public GameObject trigger;

	private Vector3 previousPosition;
	private float previousSize;

	void Awake () {
		previousPosition = camera.transform.position;
		previousSize = camera.orthographicSize;
	}

	// Use this for initialization

	void Start () {
		trigger.SetActive(true);
		camera.transform.position = cameraPosition;
		camera.orthographicSize = 5.3f;
	}

	void OnDestroy() {
		camera.transform.position = previousPosition;
		camera.orthographicSize = previousSize;
	}
}
