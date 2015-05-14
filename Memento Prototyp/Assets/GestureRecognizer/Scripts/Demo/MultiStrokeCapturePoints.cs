using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;

public class MultiStrokeCapturePoints : MonoBehaviour {

	[Tooltip("Disable or enable gesture recognition")]
	public bool isEnabled = true;

	[Tooltip("Overwrite the XML file in persistent data path")]
	public bool forceCopy = false;

	[Tooltip("The name of the gesture library to load. Do NOT include '.xml'")]
	public string libraryToLoad = "multistroke_shapes";

	[Tooltip("A new point will be placed if it is this further than the last point.")]
	public float distanceBetweenPoints = 10f;

	[Tooltip("Minimum amount of points required to recognize a multistroke.")]
	public int minimumPointsToRecognize = 10;

	[Tooltip("Material for the line renderer.")]
	public Material lineMaterial;

	[Tooltip("Start thickness of the gesture.")]
	public float startThickness = 0.5f;

	[Tooltip("End thickness of the gesture.")]
	public float endThickness = 0.05f;

	[Tooltip("Start color of the gesture.")]
	public Color startColor = Color.red;

	[Tooltip("End color of the gesture.")]
	public Color endColor = Color.white;

	// Current platform.
	RuntimePlatform platform;

	// Line renderer component.
	LineRenderer currentStrokeRenderer;

	// The position of the point on the screen.
	Vector3 virtualKeyPosition = Vector2.zero;

	// A new point.
	Vector2 point = Vector2.zero;

	// Last added point.
	Vector2 lastPoint = Vector2.zero;

	// Vertex count of the line renderer.
	int vertexCount = 0;

	// Last stroke's ID.
	int lastStrokeID = -1;

	// Loaded multiStroke library.
	MultiStrokeLibrary ml;

	// Captured points
	List<MultiStrokePoint> multiStrokePoints;

	// Recognized multiStroke.
	MultiStroke multiStroke;

	// Result.
	Result result;

	// Strokes.
	List<GameObject> strokes;

	// Draw area.
	Rect drawArea;

	// Message to show.
	string message;

	// New multistroke name.
	string newMultiStrokeName = "";

	// If a multistroke is recognized, we will clear strokes on the screen OnMouseButtonDown
	bool isRecognized;


	// Get the platform.
	void Awake() {
		platform = Application.platform;
	}


	// Load the library.
	void Start() {
		ml = new MultiStrokeLibrary(libraryToLoad, forceCopy);
		strokes = new List<GameObject>();
		drawArea = new Rect(0, 0, Screen.width - 370, Screen.height);
		multiStrokePoints = new List<MultiStrokePoint>();
	}


    void Update() {

		if (isEnabled) {

			// If it is a touch device, get the touch position
			// if it is not, get the mouse position
			if (Utility.IsTouchDevice()) {
				if (Input.touchCount > 0) {
					virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
				}
			} else {
				if (Input.GetMouseButton(0)) {
					virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
				}
			}

			if (drawArea.Contains(virtualKeyPosition)) {
				// It is not necessary to track the touch from this point on,
				// because it is already registered, and GetMouseButton event 
				// also fires on touch devices
				if (Input.GetMouseButtonDown(0)) {

					if (isRecognized) {
						ClearGesture();
					}

					point = Vector2.zero;
					lastPoint = Vector2.zero;
					AddStroke();
				}

				// It is not necessary to track the touch from this point on,
				// because it is already registered, and GetMouseButton event 
				// also fires on touch devices
				if (Input.GetMouseButton(0)) {

					point = new Vector2(virtualKeyPosition.x, -virtualKeyPosition.y);

					// Register this point only if the point list is empty or current point
					// is far enough than the last point. This ensures that the gesture looks
					// good on the screen. Moreover, it is good to not overpopulate the screen
					// with so much points.
					if (Vector2.Distance(point, lastPoint) > distanceBetweenPoints) {
						multiStrokePoints.Add(new MultiStrokePoint(point, lastStrokeID));
						lastPoint = point;

						currentStrokeRenderer.SetVertexCount(++vertexCount);
						currentStrokeRenderer.SetPosition(vertexCount - 1, Utility.WorldCoordinateForGesturePoint(virtualKeyPosition));
					}

				}

				// Capture the gesture, recognize it, fire the recognition event,
				// and clear the gesture from the screen.
				if (Input.GetMouseButtonDown(1)) {
					Recognize();
				} 
			}
		}

    }


    void OnGUI() {
        GUI.Box(drawArea, "Draw Area\nLeft click and drag to draw, right click or \"Recognize\" to recognize");

        GUI.skin.label.fontSize = 20;
        GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);
		
        GUI.Label(new Rect(Screen.width - 340, 10, 70, 30), "Add as: ");
        newMultiStrokeName = GUI.TextField(new Rect(Screen.width - 270, 10, 200, 30), newMultiStrokeName);

        if (GUI.Button(new Rect(Screen.width - 60, 10, 50, 30), "Add")) {
			multiStroke = new MultiStroke(multiStrokePoints.ToArray(), newMultiStrokeName);
			ml.AddMultiStroke(multiStroke);
        }

		if (GUI.Button(new Rect(Screen.width - 260, 90, 250, 30), "Recognize")) {
			Recognize();
		}

		if (GUI.Button(new Rect(Screen.width - 260, 50, 250, 30), "Clear")) {
			ClearGesture();
		}
    }


	void Recognize() {
		if (multiStrokePoints.Count > minimumPointsToRecognize) {
			multiStroke = new MultiStroke(multiStrokePoints.ToArray());

			result = multiStroke.Recognize(ml);
			isRecognized = true;

			message = result.Name + "; " + result.Score;
		}
	}


	/// <summary>
	/// Remove the gesture from the screen.
	/// </summary>
	void ClearGesture() {

		isRecognized = false;
		vertexCount = 0;
		lastStrokeID = -1;
		multiStrokePoints.Clear();

		for (int i = strokes.Count - 1; i >= 0; i--) {
			Destroy(strokes[i]);
		}

		strokes.Clear();
	}


	/// <summary>
	/// Add a new stroke to gesture
	/// </summary>
	void AddStroke() {
		lastStrokeID++;
		vertexCount = 0;
		GameObject newStroke = new GameObject();
		newStroke.name = "Stroke " + lastStrokeID;
		newStroke.transform.parent = this.transform;
		currentStrokeRenderer = newStroke.AddComponent<LineRenderer>();
		currentStrokeRenderer.SetVertexCount(0);
		currentStrokeRenderer.material = lineMaterial;
		currentStrokeRenderer.SetColors(startColor, endColor);
		currentStrokeRenderer.SetWidth(startThickness, endThickness);
		strokes.Add(newStroke);
	}

}
