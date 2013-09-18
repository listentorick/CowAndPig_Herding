using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	

	
	void Update () {
		
		if (Input.GetMouseButton(0)==true) {
		  Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                 RaycastHit Hit;
          Vector3 targetPos;
 
          if (Physics.Raycast (ray, out Hit, 1000)) 
          {
 
            //  if (Hit.collider.gameObject.layer=="Terrain") 
              //{
                 Debug.DrawRay (Camera.mainCamera.transform.position, Hit.point, Color.red);
              //}
 
          }
          targetPos = Hit.point;
			targetPos.y= 0;
        //  targetPos.y = (float)(transform.position.y + 1.3);
          //targetPos.z -= 1;
		
		this.transform.position = targetPos;
				
			Debug.Log(targetPos.x + " " + targetPos.y + " " + targetPos.z);
			
			
		}
		
		//if (Input.GetMouseButton(0)==true) {
		  //   Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,System.Math.Abs(Camera.mainCamera.transform.position.z)));
		//	this.transform.position = mousePosition;
		//}
	
	}
}
