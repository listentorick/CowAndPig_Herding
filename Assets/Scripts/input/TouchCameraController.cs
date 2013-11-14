using UnityEngine;
using System.Collections;

public class TouchCameraController : IIsomentricCameraController {

   	// The ID of the touch that began the scroll.
   	int ScrollTouchID = -1;
   	// The position of that initial touch
   	Vector2 ScrollTouchOrigin;
	
	RaycastHit hit;
 
   	public Vector2 CalculateTranslation(Transform transform)
   	{
		
		if(Input.touchCount>1) {
			return new Vector2(0,0);
		}
		
   	
		float translationX = 0;
		float translationY = 0;
		
       foreach(Touch T in Input.touches)
       {
          //Note down the touch ID and position when the touch begins...
          if (T.phase == TouchPhase.Began)
          {                        
      	 	if (ScrollTouchID == -1)
       		{
          		ScrollTouchID = T.fingerId;    
          		ScrollTouchOrigin = T.position;    
            }
          }
          //Forget it when the touch ends
          if ((T.phase == TouchPhase.Ended) || (T.phase == TouchPhase.Canceled))
          {                        
            ScrollTouchID = -1;  
          }
          if (T.phase == TouchPhase.Moved)
          {
                //If the finger has moved and it's the finger that started the touch, move the camera along the Y axis.
           	if (T.fingerId == ScrollTouchID)
           	{
            	//Vector3 CameraPos = Camera.main.transform;
					
				translationX  = T.deltaPosition.x;
    			translationY = T.deltaPosition.y;
					
				
				
				//return translation;
				//transform.Translate(translationX + translationY, 0, translationY - translationX); 	
					
					
          	}
          }
       }
		
		return new Vector2(translationX,translationY);
   	}
	
	public Vector2 CalculateRotation(Transform transform) {
		
		if(Input.touchCount==1) {
			return new Vector2(0,0);
		}
		
		float translationX = 0;
		float translationY = 0;
		
		foreach(Touch T in Input.touches)
		{
			//Note down the touch ID and position when the touch begins...
			if (T.phase == TouchPhase.Began)
			{                        
				if (ScrollTouchID == -1)
				{
					ScrollTouchID = T.fingerId;    
					ScrollTouchOrigin = T.position;    
				}
			}
			//Forget it when the touch ends
			if ((T.phase == TouchPhase.Ended) || (T.phase == TouchPhase.Canceled))
			{                        
				ScrollTouchID = -1;  
			}
			if (T.phase == TouchPhase.Moved)
			{
			    //If the finger has moved and it's the finger that started the touch, move the camera along the Y axis.
				if (T.fingerId == ScrollTouchID)
				{
					//Vector3 CameraPos = Camera.main.transform;
						
					translationX  = T.deltaPosition.x;
					translationY = T.deltaPosition.y;
				
				}
			}
		}
			
		Vector2 changeInRotation = new Vector2(translationX,translationY);
        return changeInRotation;
	
	}
}
