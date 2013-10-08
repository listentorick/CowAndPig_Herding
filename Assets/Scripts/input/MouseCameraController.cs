using UnityEngine;
using System.Collections;

public class MouseCameraController : IIsomentricCameraController {

	public void TranslateCamera(Transform transform)
   	{
		float translationX  = Input.GetAxis("Horizontal");
    	float translationY = Input.GetAxis("Vertical");
		transform.Translate(translationX + translationY, 0, translationY - translationX); 
	}
}
