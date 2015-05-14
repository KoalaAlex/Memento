using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ICode;
using ArrayUtility=ICode.ArrayUtility;

namespace ICode.FSMEditor{
	[System.Serializable]
	public class VariableEditor  {
		private const float ElementHeight = 22f;
		[SerializeField]
		private Vector2 scrollPosition;

		public VariableEditor(){
			scrollPosition.y = Mathf.Infinity;	
		}

		private bool DoHeader(){
			bool foldOut = EditorPrefs.GetBool ("FsmVariables", true);
			Rect rect = GUILayoutUtility.GetRect (new GUIContent ("Variables"), FsmEditorStyles.variableHeader, GUILayout.ExpandWidth (true));
			rect.x -= 1;
			rect.width += 2;
			Rect rect2 = new Rect (rect.width-10,rect.y+2,25,25);

			EventType eventType = FsmEditorUtility.ReserveEvent (rect2);
			if (GUI.Button (rect, "Variables", FsmEditorStyles.variableHeader)) {
				if(Event.current.button==0){
					EditorPrefs.SetBool ("FsmVariables", !foldOut);	
				}
			}

			FsmEditorUtility.ReleaseEvent (eventType);

			if (GUI.Button (rect2, FsmEditorStyles.toolbarPlus,FsmEditorStyles.label)) {
				FsmGUIUtility.SubclassMenu<FsmVariable>(CreateVariable);
				Event.current.Use();	
			}
			return foldOut;
		}

		public void DoGUI(Rect position){
			GUILayout.BeginArea (position,EditorStyles.inspectorDefaultMargins);
			GUILayout.FlexibleSpace ();
			GUILayout.BeginVertical ();

			if (DoHeader()) {
				scrollPosition=GUILayout.BeginScrollView(scrollPosition);
				GUILayout.BeginVertical(FsmEditorStyles.elementBackground);

				if(FsmEditor.Root != null && FsmEditor.Root.VisibleVariables.Length > 0){
					for(int i=0; i< FsmEditor.Root.VisibleVariables.Length;i++){
						FsmVariable variable=FsmEditor.Root.VisibleVariables[i];
						DoVariable(variable);
					}
				}else{
					GUILayout.Label("List is Empty");
				}
				GUILayout.EndVertical ();
				GUILayout.EndScrollView();
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}

		private void DoVariable(FsmVariable variable){
			SerializedObject serializedObject = new SerializedObject (variable);
			SerializedProperty nameProperty = serializedObject.FindProperty ("name");
			SerializedProperty valueProperty = serializedObject.FindProperty ("value");

			GUILayout.BeginHorizontal ();
			serializedObject.Update ();
			EditorGUILayout.PropertyField(nameProperty,GUIContent.none);
			if (valueProperty != null) {
				if (valueProperty.propertyType == SerializedPropertyType.Boolean) {
					EditorGUILayout.PropertyField (valueProperty, GUIContent.none, GUILayout.Width (17));	
				} else {
					EditorGUILayout.PropertyField (valueProperty, GUIContent.none);
				}
			}
			serializedObject.ApplyModifiedProperties ();
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button (FsmEditorStyles.toolbarMinus,FsmEditorStyles.label)) {
				FsmEditor.Root.Variables=ArrayUtility.Remove<FsmVariable>(FsmEditor.Root.Variables,variable);
				UnityEngine.Object.DestroyImmediate(variable,true);
				AssetDatabase.SaveAssets();
			}
			GUILayout.EndHorizontal ();

		}

		public float GetEditorHeight(){
			int cnt =  FsmEditor.Root != null?FsmEditor.Root.VisibleVariables.Length:0;
			bool foldOut = EditorPrefs.GetBool ("FsmVariables", false);
			return VariableEditor.ElementHeight + (foldOut?VariableEditor.ElementHeight * cnt + (cnt == 0 ? 1 : 0) * VariableEditor.ElementHeight:0);
		}

		private void CreateVariable(Type type){
			if (FsmEditor.Root != null) {
				FsmVariable variable = (FsmVariable)ScriptableObject.CreateInstance (type);
				variable.Name = "New " + type.Name.Replace ("Fsm", "");
				variable.IsShared=true;
				variable.hideFlags = HideFlags.HideInHierarchy;
				FsmEditor.Root.Variables = ArrayUtility.Add<FsmVariable> (FsmEditor.Root.Variables, variable);
				scrollPosition.y = Mathf.Infinity;
				if (EditorUtility.IsPersistent (FsmEditor.Root)) {
					AssetDatabase.AddObjectToAsset (variable, FsmEditor.Root);
					AssetDatabase.SaveAssets ();
				}
			}
		}
	}
}