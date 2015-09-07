using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FogRadiusTrigger : MonoBehaviour {
	public Transform prefabGolemDeath;
	private UnityStandardAssets.CrossPlatformInput.MoveMultiTouch multiTouch;
	private bool inRadius;
	private float distance = 0;
	public float maxDistance = 20f;
	private float prevPosX;
	private ParticleSystem particelSystem;
	private Color tempColor;
	private Text percentUI;
	private float percent;
	private bool reached = false;
	private Quaternion tempRot;
	private int modusGolemFall = 0;

	void Start(){
		multiTouch = globalVariables.levi.GetComponent<UnityStandardAssets.CrossPlatformInput.MoveMultiTouch>();
		particelSystem = gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
		percentUI = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Text>();
	}

	void OnTriggerEnter2D(){
		inRadius = true;
	}

	void OnTriggerExit2D(){
		inRadius = false;
	}

	void Update(){
		if(!reached){
			if(multiTouch.panelActive && inRadius){
				distance += Mathf.Abs(globalVariables.levi.transform.position.x - prevPosX) * Time.deltaTime * 22f;
				if(distance > maxDistance){
					distance = maxDistance;
				}
				print ("DAMAGE" + distance);
			}
			prevPosX = globalVariables.levi.transform.position.x;
			tempColor = particelSystem.startColor;
			particelSystem.startColor = new Color(tempColor.r, tempColor.g, tempColor.b, distance/maxDistance);
			percent = (distance/maxDistance);
			if(percent < 0f){
				percent = 0f;
			}
			else if(percent >= 1f){
				percent = 1f;
				reached = true;
			}
			percentUI.text = Mathf.Round(percent * 100) + "%";
			if(distance > 0){
				RemoveDistanceAfterTime();
			}
		}
		else{
			GolemFallAnimation();
		}
	}

	void RemoveDistanceAfterTime(){
		distance = distance - 6f * Time.deltaTime;
	}

	void GolemFallAnimation(){
		tempRot = globalVariables.golem.transform.rotation;
		ModusFall();
	}

	void ModusFall(){
		switch(modusGolemFall){
		case 0:
			globalVariables.golem.transform.rotation = new Quaternion(tempRot.x, tempRot.y, tempRot.z + 0.1f * Time.deltaTime, tempRot.w);
			if(tempRot.z > 0.1f){
				modusGolemFall++;
			}
			break;
		case 1:
			globalVariables.golem.transform.rotation = new Quaternion(tempRot.x, tempRot.y, tempRot.z - 0.1f * Time.deltaTime, tempRot.w);
			if(tempRot.z < -0.1f){
				globalVariables.levi.GetComponent<UnityStandardAssets.CrossPlatformInput.MoveMultiTouch>().MoveWithLevi();
				globalVariables.isZoomOut = true;
				modusGolemFall++;
			}
			break;
		case 2:
			globalVariables.golem.transform.rotation = new Quaternion(tempRot.x, tempRot.y, tempRot.z + 0.2f * Time.deltaTime, tempRot.w);
			if(tempRot.z > 0.1f){
				modusGolemFall++;
			}
			break;
		case 3:
			globalVariables.golem.transform.rotation = new Quaternion(tempRot.x, tempRot.y, tempRot.z + 0.3f * Time.deltaTime, tempRot.w);
			print ("ROT " + tempRot.z);
			if(tempRot.z > 0.68f){
				Instantiate(prefabGolemDeath, prefabGolemDeath.transform.position, Quaternion.identity);
				// Change To Maya
				modusGolemFall++;
				globalVariables.player.GetComponent<healthControle>().StartWonScreen();
				globalVariables.golem.SetActive(false);
			}
			break;
		default:
			break;
		}
	}
}
