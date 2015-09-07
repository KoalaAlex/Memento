using UnityEngine;
using System.Collections;
using MadLevelManager;

public class endLevel : MonoBehaviour {

	public GameObject destroyOnLeave;

	void OnTriggerEnter2D(Collider2D coll){
		/* // Give 1 Star
		MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
		// Complete CUrrent Level
		if(destroyOnLeave != null){
			Destroy(destroyOnLeave);
		}
		MadLevelProfile.SetCompleted(MadLevel.currentLevelName, true);
		// Go to Menu
		MadLevel.LoadFirst();
		print ("END LEVEL!!!"); */

		Application.LoadLevel ("_SandLevel1-1");
	}
}
