//Example script

var fireball:ParticleSystem;		//Place fireball prefab
var speed:float=200;	

function OnMouseDown (){
	
	fireball.transform.position = Vector3(Random.Range(-4,4),10,Random.Range(3,13));
	fireball.Play();

}