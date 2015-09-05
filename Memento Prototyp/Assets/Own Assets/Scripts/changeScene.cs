using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {

	public void changeLevel(int index){
		Application.LoadLevel(index);
	}

	public void changeLevel(string levelName) {
		Application.LoadLevel(levelName);
	}

}
