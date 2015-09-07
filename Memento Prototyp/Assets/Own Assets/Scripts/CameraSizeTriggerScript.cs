using UnityEngine;
using System.Collections;

public class CameraSizeTriggerScript : MonoBehaviour {
	public float minSize = 4.5f;
	public float maxSize = 11f;
	public float minSizePivot = 1f;
	public float maxSizePivot = 7f;

	private float posXStart;
	private float posXEnd;

	private float posXPlayer;
	private Vector3 tmpPos;

	private float lerpValue;

	void Start(){
		posXStart = transform.GetChild (0).transform.position.x - 0.5f;
		posXEnd = transform.GetChild(1).transform.position.x - 0.5f;
	}
	
	public void changeCamSize(){
		posXPlayer = globalVariables.player.transform.position.x;

		lerpValue = Mathf.InverseLerp(posXStart, posXEnd, posXPlayer);

		Camera.main.orthographicSize = Mathf.Lerp(minSize, maxSize, lerpValue);

		tmpPos = globalVariables.cameraPivot.transform.localPosition;
		globalVariables.cameraPivot.transform.localPosition = new Vector3(tmpPos.x, Mathf.Lerp(minSizePivot, maxSizePivot, lerpValue), tmpPos.z);
	}

	void Update(){
		// kann man auch mit prefab machen (wenigr Speicher verbrauch)
		if(globalVariables.nearBossEnemy){
			changeCamSize();
		}
		if(globalVariables.isZoom){
			StartCoroutine("ZoomIn");
			globalVariables.isZoom = false;
		}
		if(globalVariables.isZoomOut){
			StartCoroutine("ZoomOut");
			globalVariables.isZoomOut = false;
		}
	}

	IEnumerator ZoomIn(){
		float lerpValue = 1f;
		while(lerpValue > 0f){
			Camera.main.orthographicSize = Mathf.Lerp(minSize, maxSize, lerpValue);
			tmpPos = globalVariables.cameraPivot.transform.localPosition;
			globalVariables.cameraPivot.transform.localPosition = new Vector3(tmpPos.x, Mathf.Lerp(minSizePivot, maxSizePivot, lerpValue), tmpPos.z);
			lerpValue = lerpValue - 0.03f;
			yield return new WaitForSeconds(0.02f);
		}
		globalVariables.helperTextUI.transform.GetChild(1).gameObject.SetActive(false);
		globalVariables.helperTextUI.transform.GetChild(0).gameObject.SetActive(true);
		globalVariables.helperTextUI.SetActive(true);
	}

	IEnumerator ZoomOut(){
		float lerpValue = 0f;
		while(lerpValue < 1f){
			Camera.main.orthographicSize = Mathf.Lerp(minSize, maxSize, lerpValue);
			tmpPos = globalVariables.cameraPivot.transform.localPosition;
			globalVariables.cameraPivot.transform.localPosition = new Vector3(tmpPos.x, Mathf.Lerp(minSizePivot, maxSizePivot, lerpValue), tmpPos.z);
			lerpValue = lerpValue + 0.03f;
			yield return new WaitForSeconds(0.02f);
			ClimbManagerScript.DeactivateClimb();
		}
	}
}
