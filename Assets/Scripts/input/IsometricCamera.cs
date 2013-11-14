using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour {
	
	public Terrain terrain;
	public IIsomentricCameraController controller;
	public Momentifier momentifierX;
	public Momentifier momentifierY;
	public Momentifier momentifierHeight;
	private Quaternion originalRotation;
	float rotationX = 0F;
	float rotationY = 0F;
	public Transform eye;
	
	private float minimumX = -360F;
	private float maximumX = 360F;
	private float minimumY = -60F;
	private float maximumY = 60F;
	
	
	// Use this for initialization
	void Start () {
		 originalRotation = this.eye.localRotation;
		controller = new TouchCameraController();
		//controller = new MouseCameraController();
		momentifierX = new Momentifier(-100f,100f,1f,1f);
		momentifierY = new Momentifier(-100f,100f,1f,1f);
		momentifierHeight = new Momentifier(0f,400f,1f,1f);
		
		
	}
	
	RaycastHit hit;
	
	// Update is called once per frame
	void Update () {
		
		if(Physics.Raycast(this.transform.position, -Vector3.up, out hit)) {
			this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 130f,this.transform.position.z);
		} 
		
		Vector2 translation = this.controller.CalculateTranslation(this.transform);
		
		float x = translation.x; //momentifierX.GetValue(translation.x);
		float y = translation.y; //momentifierY.GetValue(translation.y);
		
		float translateX = x + y;
		float translateZ = y - x;
		
		float posX = this.transform.position.x + translateX;
		float posZ = this.transform.position.z + translateZ;
		
		//lower left
		Vector3 terrainMinPos = terrain.transform.position;
		Vector3 terrainMaxPos = terrain.transform.position + terrain.terrainData.size;
		
		if(posX > terrainMaxPos.x){
			translateX = terrainMaxPos.x - this.transform.position.x;
		}
		
		if(posX < terrainMinPos.x){
			translateX = terrainMinPos.x - this.transform.position.x;
		}
		
		
		if(posZ > terrainMaxPos.z){
			translateZ = terrainMaxPos.z - this.transform.position.z;
		}
		
		if(posZ < terrainMinPos.z){
			translateZ = terrainMinPos.z - this.transform.position.z;
		}
		
		transform.Translate(translateX, 0, translateZ); 
		
		
		//now lets handle the rotation
		
		Vector2 changeInRotation = this.controller.CalculateRotation(this.transform);
		
		// Read the mouse input axis
        rotationX += changeInRotation.x;
        rotationY += changeInRotation.y;
		
		rotationX = ClampAngle (rotationX, minimumX, maximumX);
        rotationY = ClampAngle (rotationY, minimumY, maximumY);
 
 
        Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
 
        eye.localRotation = originalRotation * xQuaternion * yQuaternion;
	
				
	}
	
	public static float ClampAngle (float angle, float min, float max)
	{
	    if (angle < -360F)
	        angle += 360F;
	    if (angle > 360F)
	        angle -= 360F;
	    return Mathf.Clamp (angle, min, max);
	}
}
