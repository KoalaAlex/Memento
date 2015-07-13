using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class SwitchUI1 : MonoBehaviour {

		private MoveLight_Panel moveLightPan;
		private MoveLightAndCharacter moveLightCharc;

		private GameObject uIHolder;
		private GameObject lightHolder;

		private MoveLight_Panel goMoveLightPanel;
		private MoveLightAndCharacter goMoveCharLight;

		private GameObject singlestick;
		private GameObject dualTouch;
		private GameObject panel;
		private GameObject buttonSideway;
		private GameObject charcAndLight;

		// Use this for initialization
		void Start () {
			uIHolder = GameObject.FindGameObjectWithTag("UIHolder");
			lightHolder = GameObject.FindGameObjectWithTag("LightHolder");

			goMoveLightPanel = lightHolder.GetComponent<MoveLight_Panel>();
			goMoveCharLight = lightHolder.GetComponent<MoveLightAndCharacter>();

			singlestick = uIHolder.transform.GetChild(0).gameObject;
			dualTouch = uIHolder.transform.GetChild(1).gameObject;
			panel = uIHolder.transform.GetChild(2).gameObject;
			buttonSideway = uIHolder.transform.GetChild(3).gameObject;
			charcAndLight = uIHolder.transform.GetChild(4).gameObject;
		}

		public void activateSingleStick(){
			setAllUIFalse();
			singlestick.SetActive(true);
		}

		public void activateDualTouch(){
			setAllUIFalse();
			dualTouch.SetActive(true);
		}

		public void activatePanel(){
			setAllUIFalse();
			panel.SetActive(true);

			goMoveCharLight.enabled = false;
			goMoveLightPanel.enabled = true;
		}

		public void activateButtonSide(){
			setAllUIFalse();
			buttonSideway.SetActive(true);

			goMoveCharLight.enabled = false;
			goMoveLightPanel.enabled = true;

		}

		public void activateCharAndLight(){
			setAllUIFalse();
			charcAndLight.SetActive(true);

			goMoveLightPanel.enabled = false;
			goMoveCharLight.enabled = true;
		}

		void setAllUIFalse(){
			singlestick.SetActive(false);
			dualTouch.SetActive(false);
			panel.SetActive(false);
			buttonSideway.SetActive(false);
			charcAndLight.SetActive(false);
		}

		public void resetDialog(){
			Destroy(GameObject.FindGameObjectWithTag("DialogPlayed"));
		}
	}
}
