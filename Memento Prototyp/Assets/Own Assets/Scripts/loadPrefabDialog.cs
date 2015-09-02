using UnityEngine;
using System.Collections;

public class loadPrefabDialog : MonoBehaviour {

	public GameObject firstEnemy;
	public GameObject tutorialUI;

	private firstEnemyEncounter firstEnemyScript;

	public Transform secondDialog;
	public Transform thirdDialog;
	
	void Start(){
		firstEnemyScript = firstEnemy.GetComponent<firstEnemyEncounter>();
	}
	
	public void setFirstEnemyScriptOn(){
		firstEnemyScript.enabled = true;
	}

	public void loadSecondDialog(){
		Instantiate(secondDialog, new Vector3(0, 0, 0), Quaternion.identity);
	}

	public void loadThirdDialog(){
		Instantiate(thirdDialog, new Vector3(0, 0, 0), Quaternion.identity);
	}

	public void firstEnemyScream(){
		StartCoroutine("TimeToScreamEnemy", 2f);
	}

	public void mayaScream(){
		globalVariables.player.GetComponent<Animator>().SetTrigger("Scarry");
	}

	public void loadFirstLeviAnimation(){
		globalVariables.levi.GetComponent<Animation>().Play("LeviAction");
		StartCoroutine("TimeToDestroyEnemy", 3f);
	}

	IEnumerator TimeToDestroyEnemy(float time){
		yield return new WaitForSeconds(time);
		firstEnemy.GetComponent<firstEnemyEncounter>().DeadAnimation();
		// After that Load the Next Dialog
		loadThirdDialog();
	}

	IEnumerator TimeToScreamEnemy(float time){
		firstEnemy.GetComponent<Animator>().SetBool("Spell Cast", true);
		yield return new WaitForSeconds(time);
		firstEnemy.GetComponent<Animator>().SetBool("Spell Cast", false);
	}

	public void StartTutorialUI(){
		tutorialUI.SetActive(true);
	}

	public void EndTutorial(){

	}
}
