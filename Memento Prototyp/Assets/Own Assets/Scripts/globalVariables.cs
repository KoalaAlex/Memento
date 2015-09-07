using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class globalVariables : MonoBehaviour {

	public static GameObject player;
	public static bool nearBossEnemy = false;
	public static UnityStandardAssets.Cameras.AutoCam autoCamScript;
	public static GameObject cameraPivot;
	public static UnityStandardAssets.CrossPlatformInput.MoveMultiTouch multiTouchScript;
	public static GameObject levi;
	public static CircleCollider2D leviRadius;

	public static GameObject controlActivator;
	public static GameObject diarySitesUI;
	public static int collectedDiarySites = 0;
	public static GameObject sphereUI;
	public static int collectedSpheres = 0;

	public static BoxCollider2D leviNotMoveColl;

	public static Vector3 leviPrevPos;
	public static GameObject golemHand;
	public static GameObject quickTRight;
	public static GameObject quickTLeft;
	public static GameObject nextPlatformBottom; 
	public static bool lastPlatfrom = false;
	public static GameObject presentPlatform;
	public static GameObject climbButtonUI;
	public static GameObject helperTextUI;
	public static GameObject golem;
	public static Animator golemAnimator;
	public static bool isZoom = false;
	public static bool isZoomOut = false;
	public static Slider healthUI;

	// Sand Level
	public static GameObject Cutscene;

	// Tutorial
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		levi = GameObject.FindGameObjectWithTag("LightHolder");
		autoCamScript = GameObject.FindGameObjectWithTag("CameraRig").GetComponent<UnityStandardAssets.Cameras.AutoCam>();
		cameraPivot = GameObject.FindGameObjectWithTag("CameraPivot");
		multiTouchScript = levi.GetComponent<UnityStandardAssets.CrossPlatformInput.MoveMultiTouch>();
		leviRadius = GameObject.FindGameObjectWithTag("LeviRadius").GetComponent<CircleCollider2D>();
		leviNotMoveColl = GameObject.FindGameObjectWithTag("LeviNotMove").GetComponent<BoxCollider2D>();
		controlActivator = GameObject.FindGameObjectWithTag("ControlActivator");
		diarySitesUI = GameObject.FindGameObjectWithTag("DiarySitesUI");
		sphereUI = GameObject.FindGameObjectWithTag("SphereUI");
		healthUI = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
	}

}
