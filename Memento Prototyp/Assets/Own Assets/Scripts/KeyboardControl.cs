using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	// Nur für die Keyboard Steuerung
	// Kann Deaktiviert werden wenn auf dem IPad transferiert wird
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class KeyboardControl : MonoBehaviour {

		private PlatformerCharacter2D m_Character;
		private bool m_Jump;

		// Alex
		public static bool uiControlActive = false;

		// Use this for initialization
		void Awake () {
			m_Character = GetComponent<PlatformerCharacter2D>();
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
			bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h = Input.GetAxis("HorizontalKeyboard");
			// Alex
			if(h != 0 || m_Jump){
				uiControlActive = true;
			}
			else{
				uiControlActive = false;
			}
			// Pass all parameters to the character control script.
			m_Character.Move(h, crouch, m_Jump);
			m_Jump = false;
		}
	}
}