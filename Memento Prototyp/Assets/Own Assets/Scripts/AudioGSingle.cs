using UnityEngine;
using System.Collections;
public class AudioGSingle : MonoBehaviour {
	private static AudioGSingle instance = null;
	public static AudioGSingle Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	void OnLevelWasLoaded(int lvlNum){
		if(Application.loadedLevelName == "_Tutorial Level"){
			Destroy(this.gameObject);
		}
	}
}