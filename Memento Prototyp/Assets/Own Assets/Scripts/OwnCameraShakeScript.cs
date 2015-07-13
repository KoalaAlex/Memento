using UnityEngine;
using System.Collections;
using Thinksquirrel.Utilities;

public class OwnCameraShakeScript : MonoBehaviour {
	CameraShake m_Shake;
	bool m_ShakeGUI;
	bool m_Shake1;
	bool m_Shake2;
	bool m_MultiShake;

	// Use this for initialization
	void Start () {
		m_Shake = GetComponent<CameraShake>();
	}
	void OnGUI()
	{
		if (m_Shake)
		{
			DrawGUIArea2();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Shake.shakeType == CameraShake.ShakeType.CameraMatrix)
		{
			/* transform.parent.position = new Vector3(
				transform.parent.position.x,
				Mathf.Cos(Time.time) * .35f,
				transform.parent.position.z);
			
			transform.localPosition = new Vector3(
				Mathf.Sin(Time.time) * .5f,
				transform.localPosition.y,
				transform.localPosition.z); */
		}
		else
		{
			transform.parent.position = new Vector3(
				Mathf.Sin(Time.time) * .5f,
				Mathf.Cos(Time.time) * .35f,
				transform.parent.position.z);
			
		}
		if (m_Shake1)
		{
			m_ShakeGUI = false;
			m_Shake1 = false;
			m_Shake.Shake();
		}
		else if (m_Shake2)
		{
			m_ShakeGUI = true;
			m_Shake2 = false;
			m_Shake.Shake();
		}
	}

	void DrawGUIArea2()
	{
		if (m_ShakeGUI)
		{
			m_Shake.BeginShakeGUILayout();
		}
		else
		{
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		}
		
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		GUILayout.Space(100);
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		GUI.enabled = !m_Shake.IsShaking() && !m_MultiShake;
		
		m_Shake1 = GUILayout.Toggle(m_Shake1, "Shake (without GUI)", GUI.skin.button, GUILayout.Width(200), GUILayout.Height(50));
		
		if (m_ShakeGUI)
		{
			m_Shake.EndShakeGUILayout();
		}
		else
		{
			GUILayout.EndArea();
		}
	}

	void Explosion()
	{
		m_MultiShake = true;
		m_ShakeGUI = true;
		
		// Single shake
		var rot = new Vector3(2, .5f, 10);
		m_Shake.Shake(m_Shake.shakeType, 5, Vector3.one, rot, 0.25f, 50.0f, 0.20f, 1.0f, true, () => m_MultiShake = false);
	}

	void Footsteps()
	{
		m_ShakeGUI = true;
		m_MultiShake = true;
		
		// Sequential shakes
		
		StartCoroutine(DoFootsteps());
	}
	IEnumerator DoFootsteps()
	{
		var rot = new Vector3(2, .5f, 1);
		m_Shake.Shake(m_Shake.shakeType, 3, Vector3.one, rot, 0.02f, 50.0f, 0.50f, 1.0f, true, null);
		yield return new WaitForSeconds(1.0f);
		m_Shake.Shake(m_Shake.shakeType, 3, Vector3.one, rot, 0.03f, 50.0f, 0.50f, 1.0f, true, null);
		yield return new WaitForSeconds(1.0f);
		m_Shake.Shake(m_Shake.shakeType, 3, Vector3.one, rot * 1.5f, 0.04f, 50.0f, 0.50f, 1.0f, true, null);
		yield return new WaitForSeconds(1.0f);
		m_Shake.Shake(m_Shake.shakeType, 3, Vector3.one, rot * 2f, 0.05f, 50.0f, 0.50f, 1.0f, true, null);
		yield return new WaitForSeconds(1.0f);
		m_Shake.Shake(m_Shake.shakeType, 3, Vector3.one, rot * 2.5f, 0.06f, 50.0f, 0.50f, 1.0f, true, () => m_MultiShake = false);
	}
	
	void Bumpy()
	{
		m_ShakeGUI = true;
		m_MultiShake = true;
		
		// Multiple sequential shakes at once
		
		StartCoroutine(DoBumpy());
		StartCoroutine(DoBumpy2());
	}
	IEnumerator DoBumpy()
	{
		var rot = new Vector3(2, 2, 2);
		for(int i = 0; i < 50; i++)
		{
			m_Shake.Shake(m_Shake.shakeType, 3, Vector3.one, rot, 0.02f, 50.0f, 0.00f, 1.0f, true, null);
			yield return new WaitForSeconds(0.1f);
		}
		m_Shake.Shake(m_Shake.shakeType, 3, Vector3.one, Vector3.one, 0.02f, 50.0f, 0.00f, 1.0f, true, () => m_MultiShake = false);
	}
	IEnumerator DoBumpy2()
	{
		var rot = new Vector3(8, 1, 4);
		for (int i = 0; i < 5; i++)
		{
			yield return new WaitForSeconds(1.0f);
			m_Shake.Shake(m_Shake.shakeType, 10, Vector3.up, rot, 0.50f, 50.0f, 0.20f, 1.0f, true, null);
		}
	}
}
