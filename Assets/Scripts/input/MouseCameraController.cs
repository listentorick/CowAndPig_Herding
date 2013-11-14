using UnityEngine;
using System.Collections;

public class MouseCameraController : IIsomentricCameraController {
	
	private float sensitivityX = 15F;
	private float sensitivityY = 15F;
	

	public Vector2 CalculateTranslation(Transform transform)
   	{
		float translationX  = Input.GetAxis("Horizontal");

    	float translationY = Input.GetAxis("Vertical");
		
		if(translationX !=0 || translationY !=0 ) Debug.Log(translationX + " " + translationY);
		
		return new Vector2(translationX,translationY);
	}
	
	public Vector2 CalculateRotation(Transform transform) {
		
	//	if(Input.GetMouseButtonDown(0)) {
			return new Vector2(Input.GetAxis("Mouse X") * sensitivityX,Input.GetAxis("Mouse Y") * sensitivityY);
	//	} else {
	//		return new Vector2(0,0);
	//	}
  
		

	}
}
