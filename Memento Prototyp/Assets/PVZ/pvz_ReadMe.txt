==============================================================
  	   Parallax Scrolling VZ - by Vital Zigns
==============================================================

Summary:
--------

Parallax Scrolling VZ provides an easy-to-use framework to create parallax scrolling backgrounds in Unity. Parallax Scrolling VZ includes three types of scrolling methods:

	OffsetScrolling: constant infinite scrolling using UV texture
		wrapping
	CharacterBasedNoFollow: parallax scrolling based on character
		movement - wraps the UV Texture
	CharacterBasedCameraFollow: parallax scrolling based on character
		movement - camera follows the character and textures do not
		repeat

Features include:
----------------

- Easy-to-setup Horizontal & Vertical Parallax scrolling for 2D Games
- Uses orthographic cameras
- 3 Types of Parallax included: Infinite Texture Offset, Infinite Character
  Movement Based, and Camera Follow Character Based
- Uses texture quads
- Infinitely wrap textures along both X and Y axis
- Parallax scrolling based on character movement
- Custom inspector for texture offset scrolling
- Ability to find pixel-perfect X & Y scale for your textures
- Prefabs with camera & layers attached for easy change out
- Ability to change scrolling speed & Z-depth for each background layer
- C# scripts
- Demo scenes


Package Folders:
----------------

- Editor: The C# files associated with the customer editors used with
	PVZ_FindScale & PVZ_TextureOffset

- PVZ Background Textures & Materials: The textures & materials used in the
	scrolling prefabs. Used as placeholders and samples.

- PVZ Character Based Parallax: contains materials to create an infinite
	character based scrolling background, and a non-infinite character
	based with camera follow background.
	contains subfolders:
		ExampleScenes: demo scenes to see how to use the prefabs
		Prefabs: prefabs for the example scenes
		Scripts: C# files associate with these 2 scrolling methods

- PVZ Offset Scrolling Parallax: contains materials to create a constant
	infinite scrolling background
	contains subfolders:
		ExampleScene: demo scene to see how to use the prefabs
		Prefabs: prefabs to use with offset scrolling
		Scripts: C# files associated with offset scrolling


Installation:
------------

Import the entire package into your project via the Asset Store.
	Window->Asset Store->  Then find the asset ->Download->Import
If you have already purchased & downloaded the package onto your computer:
	Assets->Import Package->CustomPackage->


Quick Guide:
------------

We recommend watching the video tutorial at: http://vitalzigns.com/pvz/

Usage:

    - Import the entire package into your project
    - Import your background images and make sure they are:
	- in power of two size: 32,64,128,256,512,1024,2048,etc
	  (ex. 512px by 1024px)
	- texture type: texture
	- wrap mode: repeat
    - Create a material for each of your background textures
	Create->Material in the project window
    - Decide which type of Parallax you want to do


Texture Offset Parallax Usage:

    - Find Scrolling_TextureOffset prefab inside PVZ Offset Scrolling Parallax >
      Prefabs
    - Drag the prefab into your scene’s Hierarchy
    - You can deactivate the main camera in the scene or you can set culling
      mask of the Camera_Offset camera to render what you want
    - Pick the layer you want to change and drag your background material onto
      the layer in the Hierarchy - Repeat for all the layers you need
    - if you have extra layers then you can deactivate in the inspector or
      delete them
    - if you need to add another layer you can duplicate one in the scene or
      drag BackgroundLayer prefab into the hierarchy nested with the other
      layers. And then drag your material onto that new layer
    - On each background layer set the desired axis, direction & speed in the
      PVZ_Texture Offset script in the inspector
    - Change the X & Y scale of your layer depending on the size needed for your
      background (ex. if you are scrolling horizontally then you need to find
      the x scale that best fills the screen and then find the appropriate Y
      scale). The find Pixel Perfect Scale in PVZ_Texture Offset inspector area
      can help you with this.
    - The depths for Scrolling_TextureOffset prefab have been preset but you can
      change the depth of each layer by changing their Z position
    - Once you have your desired options set then Play the Scene
    - Tweak scrolling speed and/or depth if need be


Character Based No Follow Usage:

    - Find PVZ_NoCamFollow_Infinite prefab inside PVZ Character Based Parallax
      folder -> Prefabs
    - Drag the prefab into your scene’s Hierarchy
    - Deactivate the main camera
    - Pick the layer you want to change and drag your background material onto
      the layer in the Hierarchy - Repeat for all the layers you need
    - Change the X & Y scale of your layers to meet your textures needs (if you
      need help finding a pixel perfect scale then drag the PVZ_FindScale script
      found inside the Offset Parallax folder to your layer and then set either
      X or Y to your desired value and it will tell you the other)
    - Set a scrolling speed & axis inside the inspector for each layer
    - Drag your character sprite into the scene & setup your needed character
      controller & rigidbody
    - Drag your character object into the “The Character” area of the
      PVZ_Infinite_CharacterBased script in the inspector for each layer
    - deactivate the sample character & change out the ground sprite if needed
    - deactivate unused background layers
    - Play the scene
    - Tweak scrolling speed and/or depth if need be


Camera Follow Character Based Usage:

    - Find PVZ_WithCamFollow prefab inside PVZ Character Based Parallax
      folder -> Prefabs
    - Drag the prefab into your scene’s Hierarchy
    - Deactivate the main camera
    - Pick the layer you want to change and drag your background material onto
      the layer in the Hierarchy - Repeat for all the layers you need
    - Change the X & Y scale of your layers to meet your textures needs (if you
      need help finding a pixel perfect scale then drag the PVZ_FindScale script
      found inside the Offset Parallax folder to your layer and then set either
      X or Y to your desired value and it will tell you the other)
    - Set a scrolling speed & axis inside the inspector for each layer
    - Drag the desired camera into “The Cam” area in each layer (ONLY IF
      DIFFERENT THAN ATTACHED CAMERA - And if different then Camera_FollowChar
      script must be attached to the new camera)
    - Drag your character sprite into the scene & setup your needed character
      controller & rigidbody
    - Drag your character object into the “The Character” area of the
      PVZ_NotInfinite_CameraFollow script in the inspector for each layer
    - Drag your new character onto the camera’s script Camera_FollowChar inside
      the target field
    - deactivate the sample character & change out the ground sprite if needed
    - deactivate unused background layers
    - Play the scene
    - Tweak scrolling speed and/or depth if need be


Misc.
FindScale Script:

    - Drag the script onto a texture you want to use as a background
    - Change your X scale to stretch the entire width of the Game screen size
      (if you are doing horizontal scrolling, Change Y if doing Vertical)
    - Once you find a good X scale for your texture put that value into the
      Scale X field and it will give you the Y value you should use for pixel
      perfect
      (if doing Vertical scrolling, find good Y scale and insert that value in
      instead of X)

--------------------------------------------------------

For an up-to-date, detailed documentation please visit:
http://vitalzigns.com/pvz/

