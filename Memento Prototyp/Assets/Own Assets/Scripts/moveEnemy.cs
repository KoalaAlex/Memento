using UnityEngine;
using System.Collections;
using ICode;

public class moveEnemy : MonoBehaviour {

	private float m_MaxSpeed = 4f;
	private float m_JumpForce = 400f;
	private Transform player;
	private Rigidbody2D m_Rigidbody2D;
	private float move;
	// Rotation
	private Quaternion rotationEnemy;
	private bool left = true;
	private bool previouseLeft = true;
	private ICodeBehaviour behaviour;

	// Use this for initialization
	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		behaviour = this.gameObject.GetBehaviour();
	}

	public void Move()
	{
		// Check if the Caracter is Left or Right
		move = Mathf.Clamp(player.position.x - transform.position.x, -1, 1);
		// Rotate the Character
		rotationEnemy = transform.rotation;
		if(move > 0)
		{
			left = false;
		}
		else
		{
			left = true;
		}
		if(left != previouseLeft)
		{
			transform.Rotate(new Vector3(0f,180f,0f));
			previouseLeft = left;
		}
		// Move the character
		m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
	}

	// Turn if Chacater is On the other side
	public void PreAttack()
	{
		// Check if the Caracter is Left or Right
		move = Mathf.Clamp(player.position.x - transform.position.x, -1, 1);
		// Rotate the Character
		rotationEnemy = transform.rotation;
		if(move > 0)
		{
			left = false;
		}
		else
		{
			left = true;
		}
		if(left != previouseLeft)
		{
			transform.Rotate(new Vector3(0f,180f,0f));
			previouseLeft = left;
		}
	}

	public void Stop()
	{
		// Check if the Caracter is Left or Right
		move = Mathf.Clamp(player.position.x - transform.position.x, -1, 1);
		// Rotate the Character
		rotationEnemy = transform.rotation;
		if(move > 0)
		{
			left = false;
		}
		else
		{
			left = true;
		}
		if(left != previouseLeft)
		{
			transform.Rotate(new Vector3(0f,180f,0f));
			behaviour.SendEvent("Move", "");
			previouseLeft = left;
		}
	}
}
