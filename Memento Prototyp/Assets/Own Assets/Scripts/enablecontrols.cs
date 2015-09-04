using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
	public class enablecontrols : MonoBehaviour {		
		void Update() {
			GameObject.Find("Maya_Final 1").GetComponent<Platformer2DUserControl>().enabled=true;
			GameObject.Find("Maya_Final 1").GetComponent<KeyboardControl>().enabled=true;
			GameObject.Find("Maya_Final 1").GetComponent<Animator>().enabled=true;
		}
	}
}