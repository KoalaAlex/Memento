using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ICode;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class LeviControleScript : MonoBehaviour {
		
		bool moveable = false;
		
		// Move Light Auto
		public float distanceX = 0.2f;
		public float distanceY = 3.5f;
		public float speed = 1.2f;
		public bool panelActive = false;
		public float jumpSpeed = 0.7f;
		
		
		private bool active = false;
		private float startTime;
		private Vector3 vectorPlayerLight = new Vector3(0,0,0);
		private float journeyLength = 1f;
		private Transform goPlayer;
		private float temp;
		public RectTransform reTrans;
		// State Machine
		private ICodeBehaviour behaviour; 
		private Vector3 touchPositionWorld;
		
		private float prevPosY = 0f;
		
		public float offsetCharacterMove = 100f;
		
		void Start(){
			goPlayer = GameObject.FindGameObjectWithTag("Player").transform;
			behaviour = gameObject.GetBehaviour();
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
			// Touch
			/*
			if(Input.touches.Length < 1){
				panelActive = false;
				CrossPlatformInputManager.SetAxisZero("Horizontal");
			}
			*/
			// Keyboard
			panelActive = false;
			CrossPlatformInputManager.SetAxisZero("Horizontal");
		}
		
		void MoveThisLight(){
			if(Input.touches.Length > 0)
			{
				touchPositionWorld = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
				MoveCharacterWithLight(Input.touches[0].position);
			}
			else{
				touchPositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				MoveCharacterWithLight(Input.mousePosition);
			}
			transform.position = new Vector3 (touchPositionWorld.x, touchPositionWorld.y, 0f);
		}
		
		void MoveCharacterWithLight(Vector2 pos){
			// Check if less than 2 Input
			if(pos.x > Screen.width/2 + offsetCharacterMove){
				CrossPlatformInputManager.SetAxisPositive("Horizontal");
				print (pos.x);
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
	}
}