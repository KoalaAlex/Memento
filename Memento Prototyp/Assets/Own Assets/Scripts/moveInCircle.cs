using UnityEngine;
using System.Collections;

public class moveInCircle : MonoBehaviour {

	float angle = 0;
	float speed=(2*Mathf.PI)/2; //2*PI in degress is 360, so you get 5 seconds to complete a circle
	float radius=1;

	void CircleMoveInDirection(float direction)
	{
		angle += speed*Time.deltaTime * direction; //if you want to switch direction, use -= instead of +=
		transform.position = transform.position + (new Vector3(Mathf.Cos(angle)*radius, Mathf.Sin(angle)*radius, 0) * Time.deltaTime) * direction;
	}
}
