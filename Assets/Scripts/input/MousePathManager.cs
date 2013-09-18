using UnityEngine;
using System.Collections;

public class MousePathManager : BasePathManager {
	
	private bool started = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit rayHit;
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if(Physics.Raycast(ray, out rayHit)) {
			
		
			Vector3 touchPoint = rayHit.point;
		
			if (Input.GetMouseButtonDown(0)) {
			
				if(!started){
					started = true;	
					OnFirstTouch(touchPoint);
				} else {
					OnTouch(touchPoint);
				}
			}
			
			if(Input.GetKeyDown(KeyCode.Return)){
				started = false;	
				OnTouchEnd(touchPoint);
			}
			
			if(Input.GetKeyDown(KeyCode.Escape)){
				started = false;	
				OnTouchCancel();
			}
		}
	}

}
