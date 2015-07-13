using UnityEngine;
using System.Collections;

public class LightAlive : MonoBehaviour {
	public float minValue = 0.2f;
	public float maxValue = 0.9f;
	public float duration = 2f;
	private float minValueCalc;
	private float maxValueCalc;
	private float min = 0f;
	private float max = 255f;
	private float lerp;
	private float alpha;
	private Renderer rendLight;
	private Color newColor;
	public Material mat;
	// Use this for initialization
	void Start () {
		minValueCalc = Mathf.Clamp(minValue, min, max);
		maxValueCalc = Mathf.Clamp(maxValue, min, max);
		rendLight = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		alpha = Mathf.Lerp(minValueCalc, maxValueCalc, lerp);
		mat.SetColor("_Color", new Color(rendLight.material.color.r,rendLight.material.color.g,rendLight.material.color.b,alpha));
	}
}
