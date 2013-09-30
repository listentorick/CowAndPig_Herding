using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public Terrain terrain;
	
	// Use this for initialization
	void Start () {
	
	}
	
	RaycastHit hit;
	
	// Update is called once per frame
	void Update () {

    	float translationX  = Input.GetAxis("Horizontal");
    	float translationY = Input.GetAxis("Vertical");
		 
		if(Physics.Raycast(this.transform.position, -Vector3.up, out hit)) {
			//if(hit.transform.=="terrain"){
				//Debug
				//this.transform.position.y = hit.point.y + 130f;
				this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 130f,this.transform.position.z);
			//}/
		}
		
		//Vector3 position = new Vector3(translationX + translationY,0,translationY - translationX);
		
	//	float z = terrain.SampleHeight(position);
	//	if(z<130) {
	//		z = 130;
	//	}
		
		

    	this.transform.Translate(translationX + translationY, 0, translationY - translationX); 
		
		//Physics.Raycast(mTransform.position, -Vector3.up, hit))
		
	}
}
