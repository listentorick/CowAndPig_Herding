using UnityEngine;
using System.Collections;

public class TouchPathManager : BasePathManager {
	
	private bool started = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit rayHit;
		
		Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
		
		if(Physics.Raycast(ray, out rayHit)) {
			
		
			Vector3 touchPoint = rayHit.point;
		
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
			
				if(!started){
					started = true;	
					OnFirstTouch(touchPoint);
				} else {
					OnTouch(touchPoint);
				}
			}
		}
	}
	
	
}
