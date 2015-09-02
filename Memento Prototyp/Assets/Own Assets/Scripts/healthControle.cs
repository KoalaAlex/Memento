using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthControle : MonoBehaviour {
	public int startHealthPoints = 100;
	private int currentHP = 0;
	private Slider characterHealthUI;
	private bool isDead = false;
	private bool afterOneSecond = true;
	
	void Start () {
		// sucht das UI Elemenet
		characterHealthUI = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
		currentHP = startHealthPoints;
	}

	/*
	// Reduces Health every Second 
	void Update(){
		if(afterOneSecond)
		{
			StartCoroutine( reduceHealthEveryOneSecond() );
			afterOneSecond = false;
		}
	}

	private IEnumerator reduceHealthEveryOneSecond()
	{
		yield return new WaitForSeconds( 1.0f );
		characterHealthUI.value -= 0.05f;
		afterOneSecond = true;
	}
	*/

	// erhält Schaden in Höhe von dmg
	public void TakeDamage(int dmg){
		currentHP = currentHP - dmg;
		if(currentHP < 0)
		{
			isDead = true;
			currentHP = 0;
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
}
