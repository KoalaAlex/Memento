//
//		Parallax Scrolling VZ 
//---------------------------------------------------------------//
// 	 PVZ_Infinite_CharacterBased
// - scrolls attached texture based on the characters movement
// - textures wrap using UV offset
// - character is not stuck to center of camera focus
//---------------------------------------------------------------//

using UnityEngine;
using System.Collections;

public class PVZ_Infinite_CharacterBased : MonoBehaviour {

	public float scrollSpeed = 0.2F;	// speed to scroll the texture
	public bool isHorizontalScroll = true;		// determines if the parallax should be Horizontal
	public bool isVerticalScroll;		// determines if the parallax should be Vertical
	
	private Material textureMaterial;	// the material that is set to repeat itself
	private float offset;				// used to determine how much texture wrap occurs
	private GameObject thisGameObj;		// reference to the attached gameobject	
	
	public Transform theCharacter;		// reference to the Characters transform
	private Vector3 previousPos;		// keeps track of the previous position of the character


	void Awake ()
	{
		// set gameobject reference to local variable
		thisGameObj = gameObject;	
	}

	void Start () 
	{
		if(theCharacter == null)
		{
			Debug.LogError("There is no Character attached. Please assign one in the inspector.");
		}
		else 
		{
			// texture that will be wrapped
			textureMaterial = thisGameObj.GetComponent<Renderer>().material;
			// get the characters starting position as a reference
			previousPos = theCharacter.position;
		}
	}

	void Update () 
	{
		//-- Determine which axis & direction to scroll the texture --------//

		// HORIZONTAL Axis & Right-To-Left Direction
		if (isHorizontalScroll && theCharacter.position.x > previousPos.x)
		{
			// set the offset based on the last frame loading & scroll speed
			offset += Time.deltaTime * scrollSpeed;
			offset = offset % 1; 	//keep the UV offset within 0-1 space to avoid jitter
			// offset the texture
			textureMaterial.mainTextureOffset = new Vector2(offset, 0f);
			// set the characters current position to the new previous
			previousPos = theCharacter.position;
		}
		// HORIZONTAL Axis & Left-To-Right Direction
		else if (isHorizontalScroll && theCharacter.position.x < previousPos.x)
		{
			
			offset -= Time.deltaTime * scrollSpeed;
			offset = offset % 1; 	
			
			textureMaterial.mainTextureOffset = new Vector2(offset, 0f); 
			previousPos = theCharacter.position;
		}
		// VERTICAL Axis & Top-To-Bottom Direction
		if (isVerticalScroll && theCharacter.position.y > previousPos.y)
		{
			offset += Time.deltaTime * scrollSpeed;
			offset = offset % 1;
			textureMaterial.mainTextureOffset = new Vector2(0f, offset);
			previousPos = theCharacter.position;
		}
		// VERTICAL Axis & Bottom-To-Top Direction
		else if (isVerticalScroll && theCharacter.position.y < previousPos.y)
		{
			offset -= Time.deltaTime * scrollSpeed;
			offset = offset % 1;
			textureMaterial.mainTextureOffset = new Vector2(0f, offset);
			previousPos = theCharacter.position;
		}

	}
}
