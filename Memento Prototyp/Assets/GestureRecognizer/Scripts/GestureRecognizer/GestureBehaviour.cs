using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;

public class GestureBehaviour : MonoBehaviour {
    
    [Tooltip("Disable or enable gesture recognition")]
    public bool isEnabled = true;

    [Tooltip("Overwrite the XML file in persistent data path")]
    public bool forceCopy = false;

    [Tooltip("Use the faster algorithm, however default (slower) algorithm has a better scoring system")]
    public bool UseProtractor = false;

    [Tooltip("The name of the gesture library to load. Do NOT include '.xml'")]
    public string libraryToLoad = "shapes";

    [Tooltip("A new point will be placed if it is this further than the last point.")]
	public float distanceBetweenPoints = 10f;

	[Tooltip("Minimum amount of points required to recognize a gesture.")]
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
    LineRenderer gestureRenderer;

    // The position of the point on the screen.
    Vector3 virtualKeyPosition = Vector2.zero;

    // A new point.
    Vector2 point;

    // List of points that form the gesture.
    List<Vector2> points = new List<Vector2>();

    // Vertex count of the line renderer.
    int vertexCount = 0;

    // Loaded gesture library.
    GestureLibrary gl;

    // Recognized gesture.
    Gesture gesture;

    // Result.
    Result result;

    /// <summary>
    /// This is the event to subscribe to.
    /// </summary>
    /// <param name="r">Result of the recognition</param>
    public delegate void GestureEvent(Result r);
    public static event GestureEvent OnRecognition;


    // Get the platform and apply attributes to line renderer.
    void Awake() {
        platform = Application.platform;
        gestureRenderer = gameObject.AddComponent<LineRenderer>();
        gestureRenderer.SetVertexCount(0);
        gestureRenderer.material = lineMaterial;
        gestureRenderer.SetColors(startColor, endColor);
        gestureRenderer.SetWidth(startThickness, endThickness);
    }


    // Load the library.
    void Start() {
        gl = new GestureLibrary(libraryToLoad, forceCopy);
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
            if (Input.GetMouseButton(0)) {

                point = new Vector2(virtualKeyPosition.x, -virtualKeyPosition.y);

                // Register this point only if the point list is empty or current point
                // is far enough than the last point. This ensures that the gesture looks
                // good on the screen. Moreover, it is good to not overpopulate the screen
                // with so much points.
                if (points.Count == 0 ||
                    (points.Count > 0 && Vector2.Distance(point, points[points.Count - 1]) > distanceBetweenPoints)) {
                    points.Add(point);

                    gestureRenderer.SetVertexCount(++vertexCount);
                    gestureRenderer.SetPosition(vertexCount - 1, Utility.WorldCoordinateForGesturePoint(virtualKeyPosition));
                }

            }

            // Capture the gesture, recognize it, fire the recognition event,
            // and clear the gesture from the screen.
            if (Input.GetMouseButtonUp(0)) {

                if (points.Count > minimumPointsToRecognize) {
                    gesture = new Gesture(points);
                    result = gesture.Recognize(gl, UseProtractor);

                    if (OnRecognition != null) {
                        OnRecognition(result);
                    }
                }

                ClearGesture();
            }
        }

    }


    /// <summary>
    /// Remove the gesture from the screen.
    /// </summary>
    void ClearGesture() {
        points.Clear();
        gestureRenderer.SetVertexCount(0);
        vertexCount = 0;
    }
}
