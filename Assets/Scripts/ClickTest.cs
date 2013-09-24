using UnityEngine;
using System.Collections;

public class ClickTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public CollisionMapBuilder collisionMapBuilder;
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit rayHit;
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if(Physics.Raycast(ray, out rayHit)) {
			
		
			Vector3 touchPoint = rayHit.point;
		
			if (Input.GetMouseButtonDown(0)) {
				if(collisionMapBuilder.IsCollision(touchPoint)){
					Debug.Log("boom");
				}	
			}
		}
	
	}
}
