using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ICode;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class MoveMultiTouch : MonoBehaviour {
		
		bool moveable = false;
		
		// Move Light Auto
		public float distanceX = 0.2f;
		public float distanceY = 3.5f;
		public float speed = 1.2f;
		public bool panelActive = false;
		public float jumpSpeed = 0.7f;

		private bool controleLevi = false;
		private bool active = false;
		private float startTime;
		private Vector3 vectorPlayerLight = new Vector3(0,0,0);
		private float journeyLength = 1f;
		private Transform goPlayer;
		private float temp;
		private Vector3 prevPivotPosition;

		private int touchcount = 0;

		public RectTransform reTrans;
		// State Machine
		private ICodeBehaviour behaviour;
		private Vector3 touchPositionWorld;

		private float prevPosY = 0f;
		
		public float offsetCharacterMove = 100f;
		
		void Start(){
			goPlayer = GameObject.FindGameObjectWithTag("Player").transform;
			behaviour = gameObject.GetBehaviour();
			touchPositionWorld = transform.position;
			prevPivotPosition = globalVariables.cameraPivot.transform.localPosition;
		}
		
		void Update(){
			if(panelActive){
				// Set The State of State Machine
				behaviour.SetNode("LightIsMoving");
				MoveThisLight();
			}
			/* if(CrossPlatformInputManager.GetAxis("Horizontal") != 0){
				MoveCharacterWithLight(Camera.main.WorldToScreenPoint(transform.position));
			}
			*/
			MoveLightBehindPlayer();
		}
		
		public void SetMoveActive(){
			touchcount++;
			panelActive = true;
			// Reset the prevPosition to This Click Position
			if (Input.touchCount > 0) {
				prevPosY = Input.touches[0].position.y;
			}
			else{
				prevPosY = Input.mousePosition.y;
			}
		}
		
		public void SetMoveDeactive(){
			touchcount--;

			if(touchcount < 1){
				panelActive = false;
				CrossPlatformInputManager.SetAxisZero("Horizontal");
			}
			// Keyboard
			//panelActive = false;
			CrossPlatformInputManager.SetAxisZero("Horizontal");
			CrossPlatformInputManager.SetAxisZero("Climb_H");
			CrossPlatformInputManager.SetAxisZero("Climb_V");
		}
		
		void MoveThisLight(){
			if(Input.touches.Length > 0)
			{
				touchPositionWorld = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
				if(!controleLevi){
					if(!ClimbManagerScript.climb){
						MoveCharacterWithLight(Input.touches[0].position);
					}
					else {
						MoveClimbCharacterWithLevi(Input.touches[0].position);
					}
				}
			}
			else{
				touchPositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if(!controleLevi){
					if(!ClimbManagerScript.climb){
						MoveCharacterWithLight(Input.mousePosition);
					}
					else {
						MoveClimbCharacterWithLevi(Input.mousePosition);
					}
				}
			}
			// in Levi Radius
			Vector3 pos = new Vector3 (touchPositionWorld.x, touchPositionWorld.y, 0f);
			if(globalVariables.leviRadius.OverlapPoint(new Vector2(pos.x, pos.y))){
				// Not in Levi Not Move Position
				if(!globalVariables.leviNotMoveColl.OverlapPoint(new Vector2(pos.x, pos.y))){
					transform.position = pos;
				}
			}
		}
		
		void MoveCharacterWithLight(Vector2 pos){
			// Check if less than 2 Input
			if(pos.x > Screen.width/2 + offsetCharacterMove){
				CrossPlatformInputManager.SetAxisPositive("Horizontal");
			}
			else if(pos.x < Screen.width/2 - offsetCharacterMove)
			{
				CrossPlatformInputManager.SetAxisNegative("Horizontal");
			}
			else
			{
				CrossPlatformInputManager.SetAxisZero("Horizontal");
			}
			if(Input.touches.Length > 1){
				JumpByMultiTouch();
			}
		}

		//Climb 
		void MoveClimbCharacterWithLevi(Vector2 pos){
			if(pos.x > Screen.width/2 + 20){
				CrossPlatformInputManager.SetAxisPositive("Climb_H");
				print ("H+");
			}
			else if(pos.x < Screen.width/2 - 20)
			{
				CrossPlatformInputManager.SetAxisNegative("Climb_H");
				print ("H-");
			}
			else
			{
				CrossPlatformInputManager.SetAxisZero("Climb_H");
			}
			
			if(pos.y > Screen.height/2 + 20){
				CrossPlatformInputManager.SetAxisPositive("Climb_V");
				print ("V+");
			}
			else if(pos.y < Screen.height/2 - 20)
			{
				CrossPlatformInputManager.SetAxisNegative("Climb_V");
				print ("V-");
			}
			else
			{
				CrossPlatformInputManager.SetAxisZero("Climb_V");
			}
			print ("SET INPUT");
		}

		/* KeyboardControl / Platformer2DUserControl*/
		void MoveLightBehindPlayer(){
			// Falls es ein UI Input gibt, dann füre die Positionsabfrage aus -- Alex
			if(UnityStandardAssets._2D.KeyboardControl.uiControlActive){
				// Distance in X , greater than distanceX
				if(Mathf.Abs(transform.position.x - goPlayer.position.x) > distanceX){
					if(!active)
					{
						active = true;
						startTime = Time.time;
					}
					
				};
				// Distance in Y , greater than distanceY
				if(Mathf.Abs(transform.position.y - goPlayer.position.y) > distanceY){
					if(!active)
					{
						active = true;
						startTime = Time.time;
					}
				};
			}
			if(active){
				MoveForSeconds();
				// Set The State of State Machine
				behaviour.SetNode("LightIsMoving");
			}
		}
		
		void MoveForSeconds(){
			vectorPlayerLight.x = transform.position.x - (transform.position.x - goPlayer.position.x) * speed * Time.deltaTime;
			vectorPlayerLight.y = transform.position.y - (transform.position.y - goPlayer.position.y) * speed * Time.deltaTime;
			vectorPlayerLight.z = transform.position.z;
			transform.position =  vectorPlayerLight;
			if(Time.time - startTime > journeyLength){
				active = false;
			}
		}
		
		void CheckIfJump(Vector2 pos){
			float diffPos = pos.y - prevPosY;
			if(diffPos > 0f){
				print ("Velo: " + diffPos * Time.deltaTime);
				if((diffPos * Time.deltaTime) > jumpSpeed){
					CrossPlatformInputManager.SetButtonDown("Jump");
					print ("JUMP!!!!!");
				}
				else{
					CrossPlatformInputManager.SetButtonUp("Jump");
				}
			}
			prevPosY = pos.y;
		}

		void JumpByMultiTouch(){
			CrossPlatformInputManager.SetButtonDown("Jump");
			print ("JUMP!!!!!");
			CrossPlatformInputManager.SetButtonUp("Jump");
		}

		public void MoveWithLevi(){
			if(controleLevi){
				globalVariables.autoCamScript.SetTarget(globalVariables.player.transform);
				globalVariables.autoCamScript.m_MoveSpeed = 3f;
				globalVariables.autoCamScript.transform.GetChild(0).transform.localPosition = prevPivotPosition;
				globalVariables.leviRadius.transform.GetChild(0).gameObject.SetActive(false);
				globalVariables.levi.transform.FindChild("leviCollider").GetComponent<CircleCollider2D>().enabled = false;
			}
			else{
				globalVariables.autoCamScript.SetTarget(globalVariables.levi.transform);
				globalVariables.autoCamScript.m_MoveSpeed = 1f;
				globalVariables.autoCamScript.transform.GetChild(0).transform.localPosition = new Vector3(0f,2f,-3f);
				globalVariables.leviRadius.transform.GetChild(0).gameObject.SetActive(true);
				globalVariables.levi.transform.FindChild("leviCollider").GetComponent<CircleCollider2D>().enabled = true;
			}
			controleLevi = !controleLevi;
		}
	}
}