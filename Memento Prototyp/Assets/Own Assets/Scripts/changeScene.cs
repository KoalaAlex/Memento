using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {
	public string name;

	public void changeLevel(int index){
		Application.LoadLevel(index);
	}

	public void changeLevel(string levelName) {
		Application.LoadLevel(levelName);
	}

	public void changeByName(){
		Application.LoadLevel(name);
	}
}
