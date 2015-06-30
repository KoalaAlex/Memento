using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;		// This allows for the use of lists, like <GameObject>
//using pseudoSinCos;



public class verts
{
	public float angle {get;set;}
	public int location {get;set;} // 1= left end point    0= middle     -1=right endpoint
	public Vector3 pos {get;set;}
	public bool endpoint { get; set;}

}

[ExecuteInEditMode]

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]

[Serializable]

public class DynamicLight : MonoBehaviour {

	public delegate void OnReachedDelegate(GameObject[] go);
	public event OnReachedDelegate OnReachedGameObjects;

	public string version = "1.3";

	public Material lightMaterial;
	public PolygonCollider2D[] allMeshes;									// Array for all of the meshes in our scene
	public List<verts> allVertices = new List<verts>();								// Array for all of the vertices in our meshes
	public float lightRadius = 20f;
	public int lightSegments = 8;
	public LayerMask Layer; // Mesh for our light mesh
	//int Layer;

	// -- OPTIMISATIONS BOOLS --//
	public bool notifyGameObjectsReached = false;
	public bool intelliderConvex = false;
	public bool staticScene = false;		// this is for load meshes only in start function
	public static bool reloadMeshes = false;

	// -- OPTIONALS BOOLS --//
	public bool recalculateNormals = true;
	public bool flipXYtoXZ = false;
	public bool debugLines = true;

	
	[HideInInspector] public int vertexWorking;
	public float RangeAngle = 360f;


	// Private variables

	Mesh lightMesh;	
	//public PolygonCollider2D[] allMeshes;
	PolygonCollider2D[] temArrayAllMeshes;

	List <verts> tempVerts = new List<verts>(); // -- temporal vertices of whole mesh --//

	List<GameObject> objReached = new List<GameObject>(); // -- save all GO reache by current light --//


	void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position, "bright.png", true);
	}


	public void setMainMaterial(Material m){
		lightMaterial = m;
	}

	public void setLayerMask(){
		#if UNITY_EDITOR
		if(!Application.isPlaying && Layer.value <= 0){
			Layer = 1<< LayerMask.NameToLayer("ShadowLayer");
		}
		#endif
	}
	


	public void Rebuild () {


		//--mesh filter--//
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if (meshFilter==null){
			//Debug.LogError("MeshFilter not found!");
			return;
		}
		
		Mesh mesh = meshFilter.sharedMesh;
		if (mesh == null){
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}
		mesh.Clear();



		//PseudoSinCos.initPseudoSinCos();
		
		//-- Step 1: obtain all active meshes in the scene --//
		//---------------------------------------------------------------------//

		//MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();		// Add a Mesh Renderer component to the light game object so the form can become visible

		lightMesh = new Mesh();																	// create a new mesh for our light mesh
		meshFilter.mesh = lightMesh;															// Set this newly created mesh to the mesh filter
		lightMesh.name = "Light Mesh";															// Give it a name
		lightMesh.MarkDynamic ();


		reloadMeshes = true;

		
	}


	void Start(){
		//-- Set Layer mask --//
		setLayerMask();

		TablaSenoCoseno.initSenCos();

		Rebuild();

		getAllMeshes();
	}

	void Update(){


		fixedLimitations();

		if(lightMesh){


			if(!Application.isPlaying){
				getAllMeshes();
			}else{
				if(staticScene == true){
					// Only reload when is notificated 
					if(reloadMeshes == true){
						getAllMeshes();
						reloadMeshes = false;
					}
				}else{
					getAllMeshes();
				}
			}


			setLight ();
			renderLightMesh ();
			resetBounds ();
		}



	}


	void fixedLimitations(){
		gameObject.transform.localScale = Vector3.one;

		Vector3 cAngle = gameObject.transform.localEulerAngles;
		cAngle.x = 0;
		cAngle.y = 0;
		gameObject.transform.localEulerAngles = cAngle;

		// Angle
		if(RangeAngle > 360.0001f)
			RangeAngle = 360;


	}


	public void getAllMeshes(){

		// Extract all Polygon2D //
		PolygonCollider2D[] allPolygon2D = FindObjectsOfType(typeof(PolygonCollider2D)) as PolygonCollider2D[];
		//allMeshes = FindObjectsOfType(typeof(PolygonCollider2D)) as PolygonCollider2D[];


		// Extract all Box2D //
		BoxCollider2D[] allBox2dCol = FindObjectsOfType(typeof(BoxCollider2D)) as BoxCollider2D[];

		// will have all box2d points mutated to polygoncollider2d
		PolygonCollider2D[] allBox2DConverted = new PolygonCollider2D[allBox2dCol.Length];
		for(int i = 0; i< allBox2DConverted.Length; i++){

		}


		for(int i = 0; i< allBox2dCol.Length; i++){
			Vector2[] boxPoints = new Vector2[4];
			boxPoints[0] = allBox2dCol[i].offset - (allBox2dCol[i].size *.5f); // bottom left
			boxPoints[1] = new Vector2(allBox2dCol[i].offset.x - (allBox2dCol[i].size.x *.5f), allBox2dCol[i].offset.y + (allBox2dCol[i].size.y *.5f)); // top left
			boxPoints[2] = new Vector2(allBox2dCol[i].offset.x + (allBox2dCol[i].size.x *.5f), allBox2dCol[i].offset.y - (allBox2dCol[i].size.y *.5f)); // top right
			boxPoints[3] = allBox2dCol[i].offset + (allBox2dCol[i].size *.5f); // bottom right

			// convert point
			allBox2DConverted[0] = new PolygonCollider2D();
			//allBox2DConverted[i].points = new Vector2[4];
			//allBox2DConverted[0].points = boxPoints;

			//Debug.Log(allBox2DConverted[i].points);


			//add to the transformer array
			//allBox2DConverted[i] = newP;
		}



		/*

		// Put All array together
		allMeshes = new PolygonCollider2D[allPolygon2D.Length + allBox2DConverted.Length];
		for(int i = 0; i < allPolygon2D.Length; i++){
			allMeshes[i] = allPolygon2D[i];
		}
		for(int i = allPolygon2D.Length; i < (allBox2DConverted.Length + allPolygon2D.Length); i++){
			allMeshes[i] = allBox2DConverted[i - allPolygon2D.Length];
		}

		*/

		//---
		allMeshes = allPolygon2D;
		//----
	}

	void resetBounds(){
		Bounds b = lightMesh.bounds;
		b.center = Vector3.zero;
		lightMesh.bounds = b;
	}
	void setLight () {

		bool sortAngles = false;

		//objectsReached.Clear(); // sweep all last objects reached

		allVertices.Clear();// Since these lists are populated every frame, clear them first to prevent overpopulation

	



		//--Step 2: Obtain vertices for each mesh --//
		//---------------------------------------------------------------------//
	
		// las siguientes variables usadas para arregla bug de ordenamiento cuando
		// los angulos calcuados se encuentran en cuadrantes mixtos (1 y 4)
		bool lows = false; // check si hay menores a -0.5
		bool his = false; // check si hay mayores a 2.0
		float magRange = 0.15f;

		// -- CLEAR TEMPVERTS --// ver 1.1.0v
		tempVerts.Clear();



		// reset counter vertices;
		vertexWorking = 0;

		for (int m = 0; m < allMeshes.Length; m++) {
		//for (int m = 0; m < 1; m++) {
			tempVerts.Clear();
			PolygonCollider2D mf = allMeshes[m];

			// -- DELETE CASTER MANUALLY --//
			if(mf == null){
				// se ha eliminado un caster
				reloadMeshes = true;
				return;
			}

			// las siguientes variables usadas para arregla bug de ordenamiento cuando
			// los angulos calcuados se encuentran en cuadrantes mixtos (1 y 4)
			lows = false; // check si hay menores a -0.5
			his = false; // check si hay mayores a 2.0

			if(notifyGameObjectsReached == true) // work only in neccesary cases -- optimization ver 1.1.0--
				objReached.Clear();



			// Method for check every point in each collider
			// if is closer from light, any point, then add collider to work.

			bool mfInWorks = false;

			for (int i = 0; i < mf.GetTotalPointCount(); i++) {
				Vector3 worldPoint = mf.transform.TransformPoint(mf.points[i]);
				if((worldPoint - gameObject.transform.position).sqrMagnitude <= lightRadius* lightRadius){

							// -- Here check if first collider point is in Z=0 pos --// for depth position
					if(flipXYtoXZ){
						if(mf.transform.TransformPoint(mf.points[i]).y == gameObject.transform.position.y){
							mfInWorks = true;
							i = mf.GetTotalPointCount();
						}
					}else{
						if(mf.transform.TransformPoint(mf.points[i]).z == gameObject.transform.position.z){
							mfInWorks = true;
							i = mf.GetTotalPointCount();
						}
					}


						
				}
			}



			if(mfInWorks == true)

			{
				if(((1 << mf.transform.gameObject.layer) & Layer) != 0){

					// Add all vertices that interact
					vertexWorking += mf.GetTotalPointCount();

					for (int i = 0; i < mf.GetTotalPointCount(); i++) {								   // ...and for ever vertex we have of each mesh filter...

						verts v = new verts();
						
						Vector2 worldPoint = (Vector2)mf.transform.TransformPoint(mf.points[i]);
						Vector2 to = worldPoint - (Vector2)transform.position;

						// Reforma fecha 24/09/2014 (ultimo argumento lighradius X worldPoint.magnitude (expensivo pero preciso))
						RaycastHit2D ray = Physics2D.Raycast(transform.position, to, to.magnitude, Layer);
						
						
						if(ray){
							v.pos = ray.point;
							//v.pos = new Vector3(v.pos.x,v.pos.y,transform.position.z); // add depth
							//Debug.Log(v.pos + "world");
														
							if( worldPoint.sqrMagnitude >= (ray.point.sqrMagnitude - magRange) && worldPoint.sqrMagnitude <= (ray.point.sqrMagnitude + magRange) )
								v.endpoint = true;

							if(notifyGameObjectsReached == true){ // work only in neccesary cases -- optimization ver 1.1.0--
								if(360 != Mathf.RoundToInt(RangeAngle)){ 
									if (Vector3.Angle(transform.InverseTransformPoint(v.pos), Vector3.up) < RangeAngle*.5f) {	// Light angle restriction
										//-- GO reached --> adding to mail list --//
										objReached.Add(ray.collider.gameObject.transform.parent.gameObject);
									}
								}else{
									//-- GO reached --> adding to main list --//
									objReached.Add(ray.collider.gameObject.transform.parent.gameObject);
								}
							}

						}else{
							v.pos =  worldPoint;
							v.endpoint = true;
						}



						//--Convert To local space for build mesh (mesh craft only in local vertex)
						v.pos = new Vector3(v.pos.x,v.pos.y, gameObject.transform.position.z);
						if(debugLines == true)
							Debug.DrawLine(transform.position, v.pos, Color.white);	

						v.pos = transform.InverseTransformPoint(v.pos); 
						//--Calculate angle
						v.angle = getVectorAngle(true,v.pos.x, v.pos.y);
						
						
						
						// -- bookmark if an angle is lower than 0 or higher than 2f --//
						//-- helper method for fix bug on shape located in 2 or more quadrants
						if(v.angle < 0f )
							lows = true;
						
						if(v.angle > 2f)
							his = true;
						
						
						//--Add verts to the main array
						//-- AVOID EXTRA CALCULOUS OF Vector3.angle --//

						if(360 != Mathf.RoundToInt(RangeAngle)){ 
							if (Vector3.Angle(v.pos, Vector3.up) < RangeAngle*.5f) {	// Light angle restriction
								if((v.pos).sqrMagnitude <= lightRadius*lightRadius){
									tempVerts.Add(v);
									if(debugLines == true)
										Debug.DrawLine(transform.position, transform.TransformPoint(v.pos), Color.white);
								}
							}
						}else{
							if((v.pos).sqrMagnitude <= lightRadius*lightRadius){
								tempVerts.Add(v);
								if(debugLines == true)
									Debug.DrawLine(transform.position, transform.TransformPoint(v.pos), Color.white);
							}
						}


						
						
						
						if(sortAngles == false)
							sortAngles = true;
						

					}
				}
				
				
				
				
				
				// Indentify the endpoints (left and right)
				if(tempVerts.Count > 0){
					
					sortList(tempVerts); // sort first
					
					int posLowAngle = 0; // save the indice of left ray
					int posHighAngle = 0; // same last in right side
					
					//Debug.Log(lows + " " + his);
					
					if(his == true && lows == true){  //-- FIX BUG OF SORTING CUANDRANT 1-4 --//

						if(tempVerts.Count > 1){

							float lowestAngle = -1f;//tempVerts[0].angle; // init with first data
							float highestAngle = tempVerts[0].angle;
							
							
							for(int d=0; d<tempVerts.Count; d++){
								
								
								
								if(tempVerts[d].angle < 1f && tempVerts[d].angle > lowestAngle){
									lowestAngle = tempVerts[d].angle;
									posLowAngle = d;
								}
								
								if(tempVerts[d].angle > 2f && tempVerts[d].angle < highestAngle){
									highestAngle = tempVerts[d].angle;
									posHighAngle = d;
								}
							}
						}


						
						
					}else{
						//-- convencional position of ray points
						// save the indice of left ray
						posLowAngle = 0; 
						posHighAngle = tempVerts.Count-1;
						
					}

					//-- fix error when sort vertex with only 1 tempvert AND rangeAngle < 360 --//
					// --------   ver 1.0.7    ---------//
					//--------------------------------------------------------------------------//
					int endPointLimit = 2;

					if(tempVerts.Count == 1){ 
						endPointLimit = 1;
						tempVerts[0].location = 7; // --lucky se7en
						// --------------------------------------------------------------------------------------------- //
						// --------------------------------------------------------------------------------------------- //

					}else{
						// -- more than one... --//
						tempVerts[posLowAngle].location = 1; // right
						tempVerts[posHighAngle].location = -1; // left
					}

					
					
					//--Add vertices to the main meshes vertexes--//
					if(intelliderConvex == true && endPointLimit > 1){
						allVertices.Add(tempVerts[posLowAngle]);
						allVertices.Add(tempVerts[posHighAngle]);
					}else{
						allVertices.AddRange(tempVerts);
					}
					 

					
					
					
					// -- r ==0 --> right ray
					// -- r ==1 --> left ray

					 
					for(int r = 0; r<endPointLimit; r++){
						
						//-- Cast a ray in same direction continuos mode, start a last point of last ray --//
						Vector3 fromCast = new Vector3();
						bool isEndpoint = false;
						
						if(r==0){
							fromCast = transform.TransformPoint(tempVerts[posLowAngle].pos);
							isEndpoint = tempVerts[posLowAngle].endpoint;
							
						}else if(r==1){
							fromCast = transform.TransformPoint(tempVerts[posHighAngle].pos);
							isEndpoint = tempVerts[posHighAngle].endpoint;
						}
						
						
						
						
						
						if(isEndpoint == true){
							Vector2 from = (Vector2) fromCast;
							Vector2 dir = (from - (Vector2)transform.position);


							from += (dir * .001f);


							float mag = (lightRadius);// - fromCast.magnitude;
							//float mag = fromCast.magnitude;
							RaycastHit2D rayCont = Physics2D.Raycast(from, dir, mag, Layer);


							
							Vector2 hitp;
							if(rayCont){
								//-- IMPROVED REACHED OBJECTS --// VERSION 1.1.2
								hitp = rayCont.point;   //world p

								/*
								if(notifyGameObjectsReached == true){ // work only in neccesary cases -- optimization ver 1.1.0--
									if((hitp - (Vector2)transform.position ).sqrMagnitude < (lightRadius * lightRadius)){
										// Version 1.3.0 
										if(360 != Mathf.RoundToInt(RangeAngle)){ 
											if (Vector3.Angle(transform.InverseTransformPoint(hitp), Vector3.up) < RangeAngle*.5f) {	// Light angle restriction
												//-- GO reached --> adding to mail list --//
												//objReached.Add(rayCont.collider.gameObject.transform.parent.gameObject);
												Debug.Log("caca");
											}
										}else{
											//-- GO reached --> adding to mail list --//
											//objReached.Add(rayCont.collider.gameObject.transform.parent.gameObject);
										}
									}		
								}
								*/

								if(debugLines == true)
									Debug.DrawLine(fromCast, new Vector3(hitp.x, hitp.y, transform.position.z), Color.green);
							}else{
								//-- FIX ERROR WEIRD MESH WHEN ENDPOINT COLLIDE OUTSIDE RADIUS VERSION 1.1.2 --//
								//-- NEW INSTANCE OF DIR VECTOR3 ADDED --//
								Vector2 newDir = transform.InverseTransformDirection(dir);	//local p
								hitp = (Vector2)transform.TransformPoint( newDir.normalized * mag); //world p

								if(debugLines == true)
									Debug.DrawLine(fromCast, new Vector3(hitp.x, hitp.y, transform.position.z), Color.blue);
							}


							// --- VER 1.0.6 -- //
							//--- this fix magnitud of end point ray (green) ---//

							if((hitp - (Vector2)transform.position ).sqrMagnitude > (lightRadius * lightRadius)){
								//-- FIX ERROR WEIRD MESH WHEN ENDPOINT COLLIDE OUTSIDE RADIUS VERSION 1.1.2  --//
								dir = (Vector2)transform.InverseTransformDirection(dir);	//local p
								hitp = (Vector2)transform.TransformPoint( dir.normalized * mag);
							}


							Vector3 v3Hitp = new Vector3(hitp.x, hitp.y, transform.position.z);
							verts vL = new verts();
							vL.pos = (Vector3) transform.InverseTransformPoint(v3Hitp);
							vL.angle = getVectorAngle(true,vL.pos.x, vL.pos.y);
							allVertices.Add(vL);
							

						}
						
						
					}
					
					
				}

				if(notifyGameObjectsReached == true){
					//notify if not null
					if(OnReachedGameObjects != null){
						OnReachedGameObjects(objReached.ToArray());
					}
				}

				
				
			}
			}


		




		//--Step 3: Generate vectors for light cast--//
		//---------------------------------------------------------------------//
		
		int theta = 0;
		//		int amount = 360 / lightSegments;
		float amount = RangeAngle / lightSegments;
		
		
		
		for (int i = 0; i <= lightSegments; i++)  {
			
			theta = Mathf.RoundToInt(amount * i);
			if(theta >= 360) theta = 0;
			
			verts v = new verts();

			// Initialize static tables
			TablaSenoCoseno.initSenCos();

			v.pos = new Vector3((TablaSenoCoseno.SenArray[theta]), (TablaSenoCoseno.CosArray[theta]), 0); // in dregrees (previous calculate)

			Quaternion quat = Quaternion.AngleAxis(RangeAngle*.5f + transform.eulerAngles.z, Vector3.forward);
			v.pos = quat * v.pos;

			v.pos *= lightRadius;
			v.pos += transform.position;

			Vector3 to = v.pos - transform.position;
			to.z = gameObject.transform.position.z;

			 
			RaycastHit2D ray = Physics2D.Raycast(transform.position,to,lightRadius, Layer);
			//Debug.DrawLine(transform.position, to, Color.blue);
			
			if (ray && (to.z == transform.position.z)){
				v.pos = transform.InverseTransformPoint(ray.point);
				v.pos = new Vector3(v.pos.x, v.pos.y, 0);
				v.angle = getVectorAngle(true,v.pos.x, v.pos.y);
				allVertices.Add(v);

			} else {
				v.pos = transform.InverseTransformPoint(v.pos);
				v.angle = getVectorAngle(true,v.pos.x, v.pos.y);// store angle without object rotation -> consistency for sorting
				allVertices.Add(v);

			}
			if(debugLines == true)
				Debug.DrawLine(transform.position, transform.TransformPoint(new Vector3(v.pos.x,v.pos.y, 0)), Color.cyan);
			
		}



		//-- Step 4: Sort each vertice by angle (along sweep ray 0 - 2PI)--//
		//---------------------------------------------------------------------//
		//if (sortAngles == true) {
			sortList(allVertices);
		//}
		//-----------------------------------------------------------------------------


		//--auxiliar step (change order vertices close to light first in position when has same direction) --//
		float rangeAngleComparision = 0.0001f;
		for(int i = 0; i< allVertices.Count; i+=1){
			
			verts uno = allVertices[i];
			verts dos = allVertices[(i +1) % allVertices.Count];

			// -- Comparo el angulo local de cada vertex y decido si tengo que hacer un exchange-- //
			if(uno.angle >= (dos.angle-rangeAngleComparision) && uno.angle <= (dos.angle + rangeAngleComparision)){

				// -- FIX BUG 1.0.7 ( exchange when rangeAngle is less than 360)  -- //
				// ----------------------------------------------------------------- //

				if(uno.location == 7){
					//Debug.Log("7");
					if(uno.angle <= allVertices[allVertices.Count/2].angle){
						uno.location = 1;
					}else{
						uno.location = -1;
					}
				}
				if(dos.location == 7){
					//Debug.Log("7");
					if(dos.angle <= allVertices[allVertices.Count/2].angle){
						dos.location = 1;
					}else{
						dos.location = -1;
					}
				}

				//--------------------------------------------------------------------------//
				//--------------------------------------------------------------------------//


				if(dos.location == -1){ // Right Ray
					
					if(uno.pos.sqrMagnitude > dos.pos.sqrMagnitude){
							allVertices[i] = dos;
							allVertices[(i +1) % allVertices.Count] = uno;
					}
				}
				

				// ALREADY DONE!!
				if(uno.location == 1){ // Left Ray

					if(uno.pos.sqrMagnitude < dos.pos.sqrMagnitude){
						allVertices[i] = dos;
						allVertices[(i +1) % allVertices.Count] = uno;

					}
				}
				
				
				
			}


		}



	}

	void renderLightMesh(){
		//-- Step 5: fill the mesh with vertices--//
		//---------------------------------------------------------------------//
		
		//interface_touch.vertexCount = allVertices.Count; // notify to UI
		
		Vector3 []initVerticesMeshLight = new Vector3[allVertices.Count+1];
		
		initVerticesMeshLight [0] = Vector3.zero;
		
		
		for (int i = 0; i < allVertices.Count; i++) { 
			//Debug.Log(allVertices[i].angle);
			initVerticesMeshLight [i+1] = allVertices[i].pos;
			
			//if(allVertices[i].endpoint == true)
			//Debug.Log(allVertices[i].angle);
			
		}
		
		lightMesh.Clear ();
		lightMesh.vertices = initVerticesMeshLight;
		
		Vector2 [] uvs = new Vector2[initVerticesMeshLight.Length];
		for (int i = 0; i < initVerticesMeshLight.Length; i++) {

				uvs[i] = new Vector2(initVerticesMeshLight[i].x, initVerticesMeshLight[i].y);	
	
		}
		lightMesh.uv = uvs;
		
		// triangles
		int idx = 0;
		int [] triangles = new int[(allVertices.Count * 3)];
		for (int i = 0; i < (allVertices.Count*3); i+= 3) {
			
			triangles[i] = 0;
			triangles[i+1] = idx+1;
			
			
			if(i == (allVertices.Count*3)-3){
				//-- if is the last vertex (one loop)
				if(Mathf.RoundToInt(RangeAngle) == 360) {
					triangles[i+2] = 1;							// last triangle closes full round
				} else {
					triangles[i+2] = 0;							// no closing when light angle < 360°
				}
			}else{
				triangles[i+2] = idx+2; //next next vertex	
			}
			
			idx++;
		}
		
		
		lightMesh.triangles = triangles;

		if(recalculateNormals == true)
			lightMesh.RecalculateNormals();

		lightMesh.RecalculateBounds();

		GetComponent<Renderer>().sharedMaterial = lightMaterial;
	}

	void sortList(List<verts> lista){
			lista.Sort((item1, item2) => (item2.angle.CompareTo(item1.angle)));
	}

	void drawLinePerVertex(){
		for (int i = 0; i < allVertices.Count; i++)
		{
			if (i < (allVertices.Count -1))
			{
				Debug.DrawLine(allVertices [i].pos , allVertices [i+1].pos, new Color(i*0.02f, i*0.02f, i*0.02f));
			}
			else
			{
				Debug.DrawLine(allVertices [i].pos , allVertices [0].pos, new Color(i*0.02f, i*0.02f, i*0.02f));
			}
		}
	}

	float getVectorAngle(bool pseudo, float x, float y){
		float ang = 0;
		if(pseudo == true){
			ang = pseudoAngle(x, y);
		}else{
			ang = Mathf.Atan2(y, x);
		}
		return ang;
	}
	
	float pseudoAngle(float dx, float dy){
		// Hight performance for calculate angle on a vector (only for sort)
		// APROXIMATE VALUES -- NOT EXACT!! //
		float ax = Mathf.Abs (dx);
		float ay = Mathf.Abs (dy);
		float p = dy / (ax + ay);
		if (dx < 0){
			p = 2 - p;

		}
		return p;
	}

}