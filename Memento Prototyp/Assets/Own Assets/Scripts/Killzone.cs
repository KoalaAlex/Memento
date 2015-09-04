using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour {
	public Vector3 respawnPosition;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
			other.transform.position = respawnPosition;
	}
}
