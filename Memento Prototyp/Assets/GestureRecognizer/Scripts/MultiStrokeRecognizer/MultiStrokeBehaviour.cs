using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;

public class MultiStrokeBehaviour : MonoBehaviour {

	[Tooltip("Disable or enable multi stroke recognition")]
	public bool isEnabled = true;

	[Tooltip("Overwrite the XML file in persistent data path")]
	public bool forceCopy = false;

	[Tooltip("The name of the multi stroke library to load. Do NOT include '.xml'")]
	public string libraryToLoad = "multistroke_shapes";

	[Tooltip("A new point will be placed if it is this further than the last point.")]
	public float distanceBetweenPoints = 10f;

	[Tooltip("Minimum amount of points required to recognize a multistroke.")]
	public int minimumPointsToRecognize = 10;

	[Tooltip("Material for the line renderer.")]
	public Material lineMaterial;

	[Tooltip("Start thickness of the multi stroke.")]
	public float startThickness = 0.5f;

	[Tooltip("End thickness of the multi stroke.")]
	public float endThickness = 0.05f;

	[Tooltip("Start color of the multi stroke.")]
	public Color startColor = Color.red;

	[Tooltip("End color of the multi stroke.")]
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
	List<MultiStrokePoint> multiStrokePoints = new List<MultiStrokePoint>();

	// Recognized multiStroke.
	MultiStroke multiStroke;

	// Result.
	Result result;

	// Strokes.
	List<GameObject> strokes = new List<GameObject>();

	/// <summary>
	/// This is the event to subscribe to.
	/// </summary>
	/// <param name="r">Result of the recognition</param>
	public delegate void MultiStrokeEvent(Result r);
	public static event MultiStrokeEvent OnRecognition;


	// Get the platform
	void Awake() {
		platform = Application.platform;
	}


	// Load the library.
	void Start() {
		ml = new MultiStrokeLibrary(libraryToLoad, forceCopy);
		strokes = new List<GameObject>();
	}


	// Track user input and fire OnRecognition event when necessary.
	void Update() {

		// Track user input if GestureRecognition is enabled.
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

			// It is not necessary to track the touch from this point on,
			// because it is already registered, and GetMouseButton event 
			// also fires on touch devices
			if (Input.GetMouseButtonDown(0)) {
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
				// is far enough than the last point. This ensures that the multi stroke looks
				// good on the screen. Moreover, it is good to not overpopulate the screen
				// with so much points.
				if (Vector2.Distance(point, lastPoint) > distanceBetweenPoints) {
					multiStrokePoints.Add(new MultiStrokePoint(point.x, point.y, lastStrokeID));
					lastPoint = point;

					currentStrokeRenderer.SetVertexCount(++vertexCount);
					currentStrokeRenderer.SetPosition(vertexCount - 1, Utility.WorldCoordinateForGesturePoint(virtualKeyPosition));
				}

			}

			// Capture the multi stroke, recognize it, fire the recognition event,
			// and clear the multi stroke from the screen.
			if (Input.GetMouseButtonDown(1)) {

				if (multiStrokePoints.Count > minimumPointsToRecognize) {
					multiStroke = new MultiStroke(multiStrokePoints.ToArray());
					result = multiStroke.Recognize(ml);

					if (OnRecognition != null) {
						OnRecognition(result);
					}
				}

				ClearGesture();
			}
		}

	}


	/// <summary>
	/// Remove the multi stroke from the screen.
	/// </summary>
	void ClearGesture() {

		vertexCount = 0;
		lastStrokeID = -1;
		multiStrokePoints.Clear();

		for (int i = strokes.Count - 1; i >= 0; i--) {
			Destroy(strokes[i]);
		}

		strokes.Clear();
	}


	/// <summary>
	/// Add a new stroke to multi stroke
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
