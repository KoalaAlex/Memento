using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(setHandle))]
public class SolidArc : Editor {

	void OnSceneGUI () {
		Handles.color = new Color (1f,0f,1f,0.2f);
		Handles.DrawSolidArc((target as setHandle).transform.position, 
		                     new Vector3(0,0,1), 
		                     new Vector3(1,0,0), 
		                     360, 
		                     (target as setHandle).shieldArea);
	}
}