using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthControle : MonoBehaviour {
	public int startHealthPoints = 100;
	public GameObject[] killzones;
	public bool golemFight = false;
	private int currentHP = 0;
	private Slider characterHealthUI;
	private bool isDead = false;
	private bool afterOneSecond = true;
	private bool changeSpawnPoint = false;
	
	void Start () {
		// sucht das UI Elemenet
		characterHealthUI = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
		currentHP = startHealthPoints;
	}

	// erhält Schaden in Höhe von dmg
	public void TakeDamage(int dmg){
		currentHP = currentHP - dmg;
		if(currentHP <= 0)
		{
			isDead = true;
			currentHP = 100;
			KillMaya();
			if(golemFight){
				ClimbManagerScript.DeactivateClimb();
				Camera.main.orthographicSize = 4.5f;
				globalVariables.climbButtonUI.SetActive(false);
				globalVariables.cameraPivot.transform.localPosition = new Vector3(3, 1, 1);
			}
		}
		characterHealthUI.value = (float)currentHP / (float)startHealthPoints;
	}

	// stellt die Lebenspunkte wieder her in höhe von hp
	public void healHP(int hp){
		currentHP = currentHP + hp;
		if(currentHP > startHealthPoints){
			currentHP = startHealthPoints;
		}
		// zeigt die hp in einen Wert zwischen 0 und 1 an
		characterHealthUI.value = currentHP / startHealthPoints;
	}

	public void KillMaya(){
		if (!changeSpawnPoint) {
			killzones [0].GetComponent<KillzonetoCheckpoint> ().FirstKillZone (gameObject);
		} else {
			killzones [1].GetComponent<KillzonetoCheckpoint> ().FirstKillZone (gameObject);
		}
	}

	public void nexSpawnPoint(){
		changeSpawnPoint = true;
	}

	public void StartWonScreen(){
		StartCoroutine ("StartWonScreenAfter");
	}
	
	IEnumerator StartWonScreenAfter(){
		yield return new WaitForSeconds (3f);
		globalVariables.helperTextUI.SetActive (true);
		globalVariables.helperTextUI.GetComponent<Button> ().enabled = false;
		globalVariables.helperTextUI.transform.GetChild (0).gameObject.SetActive (false);
		globalVariables.helperTextUI.transform.GetChild (1).gameObject.SetActive (false);
		globalVariables.helperTextUI.transform.GetChild (2).gameObject.SetActive (true);
	}
}
