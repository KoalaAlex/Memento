using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	// Nur für die Keyboard Steuerung
	// Kann Deaktiviert werden wenn auf dem IPad transferiert wird
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class KeyboardClimb : MonoBehaviour {

		public bool top = false;
		public bool bottom = false;

		private PlatformerCharacter2D m_Character;
		private bool m_Jump;
		public float speedClimb = 2f;

		private Animator m_Anim;
		
		// wird für die Lichtbewegung verwendet (MoveLight)
		public static bool uiControlActive = false;
		
		// Use this for initialization
		void Awake () {
			m_Character = GetComponent<PlatformerCharacter2D>();
			m_Anim = GetComponent<Animator>();
		}
		
		private void Update()
		{
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = Input.GetButtonDown("Jump");
			}
		}
		
		
		private void FixedUpdate()
		{
			// Read the inputs.
			float vertical = CrossPlatformInputManager.GetAxis("Climb_V");
			float horizontal = CrossPlatformInputManager.GetAxis("Climb_H");

			/*if(top){
				if(vertical > 0f){
					vertical = 0f;
				}
			} */

			if(bottom){
				if(vertical < 0f){
					vertical = 0f;
				}
			}
			//print ("H: " + horizontal);
			m_Anim.SetFloat("hSpeed", horizontal);
			//print ("V: " + vertical);
			// Pass all parameters to the character control script.
			vertical = speedClimb * vertical;
			m_Character.MoveClimb(horizontal, vertical);
		}
	}
}