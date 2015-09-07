using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
	public class disablecontrols : MonoBehaviour {
		void Update() {
			GameObject.Find("Maya_Final 1").GetComponent<Platformer2DUserControl>().enabled=false;
			GameObject.Find("Maya_Final 1").GetComponent<Animator>().enabled=false;
		}
	}
}