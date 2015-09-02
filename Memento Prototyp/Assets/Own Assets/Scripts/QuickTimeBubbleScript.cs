using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuickTimeBubbleScript : MonoBehaviour {
	public bool swiped = false;
	public GameObject bubbleBefore = null;

	public void setSwipedToTrue(){
		if(bubbleBefore == null){
			swiped = true;
			changeBubbleColor();
		}
		else if(bubbleBefore.GetComponent<QuickTimeBubbleScript>().swiped == true){
			swiped = true;
			changeBubbleColor();
		}
	}

	void changeBubbleColor(){
		GetComponent<Image>().color = Color.red;
	}

	public void resetBubbleColor(){
		GetComponent<Image>().color = Color.white;
	}
}
