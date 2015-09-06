using UnityEngine;
using System.Collections;

public class SandLevelCutscene1_2 : MonoBehaviour {

	public Vector3 cameraPosition;
	public Camera camera;
	public GameObject particle;
	
	private Vector3 previousPosition;
	private float previousSize;
	
	void Awake () {
		previousPosition = camera.transform.position;
		previousSize = camera.orthographicSize;
	}
	
	// Use this for initialization
	
	void Start () {
		camera.transform.position = cameraPosition;
		camera.orthographicSize = 5.3f;
		particle.GetComponent<ParticleSystem> ().loop = false;
		StartCoroutine ("ActivateAfter");
	}
	
	void OnDestroy() {
		camera.transform.position = previousPosition;
		camera.orthographicSize = previousSize;
	}

	IEnumerator ActivateAfter(){
		yield return new WaitForSeconds(5f);
		GameObject.Destroy (this.gameObject);
	}
}
