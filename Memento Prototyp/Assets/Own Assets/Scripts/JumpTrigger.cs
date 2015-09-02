using UnityEngine;
using System.Collections;
using ICode;

public class JumpTrigger : MonoBehaviour {
	private ICodeBehaviour behaviour;

	void OnTriggerEnter2D(Collider2D coll)
	{
		print(coll.gameObject.transform.position);
		behaviour = coll.gameObject.transform.parent.gameObject.GetBehaviour();
		behaviour.SendEvent("Jump", "");
	}
}
