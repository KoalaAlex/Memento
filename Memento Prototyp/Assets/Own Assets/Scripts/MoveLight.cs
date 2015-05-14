using UnityEngine;
using System.Collections;

public class MoveLight : MonoBehaviour {
	Vector2 scrollDelta;
	Vector2 scrollDistance;
	bool moveable = false;

	void Start(){
		scrollDistance.x = transform.position.x;
		scrollDistance.y = transform.position.y;
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
				transform.position = new Vector3 (touchPositionWorld.x, touchPositionWorld.y - transform.localScale.y, 0f);
				/* scrollDistance.x = scrollDistance.x + scrollDelta.x * Time.deltaTime * 0.5f;
			scrollDistance.y = scrollDistance.y + scrollDelta.y * Time.deltaTime * 0.5f;
			transform.position = new Vector3 (scrollDistance.x, scrollDistance.y, 0); */
			}
		}
	}
}
