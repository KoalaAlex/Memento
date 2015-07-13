using UnityEngine;
using System.Collections;

public class MoveLightKeyboard : MonoBehaviour {

	bool moveable = false;

	// Move Light Auto
	public float distanceX = 6f;
	public float distanceY = 3.5f;
	public float speed = 1.2f;
	private bool active = false;
	private float startTime;
	private Vector3 vectorPlayerLight = new Vector3(0,0,0);
	private float journeyLength = 1f;
	private Transform goPlayer;
	private float temp;
	void Start(){
		goPlayer = GameObject.FindGameObjectWithTag("Player").transform;
	}
	/*
	void Update() {
		if (Input.touchCount > 0) {
			Touch myTouch = Input.GetTouch(0);
			// Get movement of the finger since last frame
			Ray ray = Camera.main.ScreenPointToRay(myTouch.position);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100f)){
				Vector2 scrollDelta = myTouch.deltaPosition;

				scrollDistance.x = Mathf.Clamp(scrollDistance.x + scrollDelta.x * Time.deltaTime * 0.5f, -horizontalLimit, horizontalLimit);
				scrollDistance.y = Mathf.Clamp(scrollDistance.y + scrollDelta.y * Time.deltaTime * 0.5f, -verticalLimit, verticalLimit);
				transform.position = new Vector3 (scrollDistance.x, scrollDistance.y, 0);
			}
		}
	} */

	void Update(){
		if(Input.touchCount > 0){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
				Touch myTouch = Input.GetTouch(0);
				Ray ray = Camera.main.ScreenPointToRay(myTouch.position);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 100f)){
					moveable = true;
				}
			}
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
				moveable = false;
			if (moveable) {
				Touch myTouch = Input.GetTouch(0);
				Vector3 touchPositionWorld = Camera.main.ScreenToWorldPoint(myTouch.position);
				transform.position = new Vector3 (touchPositionWorld.x, touchPositionWorld.y, 0f);
			}
		}
		MoveLightBehindPlayer();
	}

	void MoveLightBehindPlayer(){
		// Falls es ein UI Input gibt, dann füre die Positionsabfrage aus -- Alex
		if(UnityStandardAssets._2D.KeyboardControl.uiControlActive){
			// Distance in X , greater than distanceX
			if(Mathf.Abs(transform.position.x - goPlayer.position.x) > distanceX){
				if(!active)
				{
					active = true;
					startTime = Time.time;
				}
			};
			// Distance in Y , greater than distanceY
			if(Mathf.Abs(transform.position.y - goPlayer.position.y) > distanceY){
				if(!active)
				{
					active = true;
					startTime = Time.time;
				}
			};
		}
		if(active){
			MoveForSeconds();
		}
	}

	void MoveForSeconds(){
		vectorPlayerLight.x = transform.position.x - (transform.position.x - goPlayer.position.x) * speed * Time.deltaTime;
		vectorPlayerLight.y = transform.position.y - (transform.position.y - goPlayer.position.y) * speed * Time.deltaTime;
		vectorPlayerLight.z = transform.position.z;
		transform.position =  vectorPlayerLight;
		if(Time.time - startTime > journeyLength){
			active = false;
		}
	}
}