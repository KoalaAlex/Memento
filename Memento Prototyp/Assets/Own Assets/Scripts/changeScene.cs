using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {
	public string name;

	private GameObject dialogOncePlayed;

	public void changeLevel(int index){
		Application.LoadLevel(index);
	}

	public void changeLevel(string levelName) {
		Application.LoadLevel(levelName);
	}

	public void changeByName(){
		Application.LoadLevel(name);
	}

	public void BackToHelga(){
		Application.OpenURL("helgamobileapp://");
		Application.Quit();
	}

	public void changeSceneAndDestroy(){
		dialogOncePlayed = GameObject.Find("DialogOncePlayed");
		if(dialogOncePlayed != null){
			Destroy(dialogOncePlayed);
		}
		Application.LoadLevel(name);
	}
}
