using UnityEngine;
using System.Collections;

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
	}


	// Boss Fight
	/*
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		golemHand = GameObject.FindGameObjectWithTag("GolemHand");
		golemHand.SetActive(false);
		quickTRight = GameObject.FindGameObjectWithTag("QT Right");
		quickTRight.SetActive(false);
		quickTLeft = GameObject.FindGameObjectWithTag("QT Left");
		quickTLeft.SetActive(false);
	}
	*/
}
