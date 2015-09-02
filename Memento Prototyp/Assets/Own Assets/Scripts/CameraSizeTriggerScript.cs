using UnityEngine;
using System.Collections;

public class CameraSizeTriggerScript : MonoBehaviour {
	public float minSize = 4.5f;
	public float maxSize = 11f;

	private float wayLenght;

	private float posXStart;
	private float posXEnd;

	private float posXPlayer;

	private float lerpValue;

	void Start(){
		posXStart = transform.GetChild(0).transform.position.x;
		posXEnd = transform.GetChild(1).transform.position.x;
		wayLenght = posXEnd - posXStart;
	}
	
	public void changeCamSize(){
		posXPlayer = globalVariables.player.transform.position.x;

		lerpValue = Mathf.InverseLerp(posXStart, posXEnd, posXPlayer);

		Camera.main.orthographicSize = Mathf.Lerp(minSize, maxSize, lerpValue);
	}

	void Update(){
		// kann man auch mit prefab machen (wenigr Speicher verbrauch)
		if(globalVariables.nearBossEnemy){
			changeCamSize();
		}
	}
}
