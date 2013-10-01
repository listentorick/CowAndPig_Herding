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
			this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 130f,this.transform.position.z);
		}
		
    	this.transform.Translate(translationX + translationY, 0, translationY - translationX); 
		
	}
}
