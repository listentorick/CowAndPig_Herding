using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour {
	
	public Terrain terrain;
	public IIsomentricCameraController controller;
	
	
	// Use this for initialization
	void Start () {
		controller = new TouchCameraController();
	}
	
	RaycastHit hit;
	
	// Update is called once per frame
	void Update () {
		
		
		
		if(Physics.Raycast(this.transform.position, -Vector3.up, out hit)) {
			this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 130f,this.transform.position.z);
		}
		
		this.controller.TranslateCamera(this.transform);
		
    //	float translationX  = Input.GetAxis("Horizontal");
    //	float translationY = Input.GetAxis("Vertical");
		 
	
		
    //	this.transform.Translate(translationX + translationY, 0, translationY - translationX); 
		
	}
}
