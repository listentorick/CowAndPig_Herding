using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreePathManager : MonoBehaviour, IPathManager {
	
	
	private bool dragging = false;
	private bool moving = false;
	private int countDrag = 0;
	private int countMove  = 0;
	private Vector3 mousePosition;
	private Vector3 mousePoint;
	private Vector3 pointCurrent;
	private Vector3 pointStore;
	
	private IList<Vector3> points = new List<Vector3>();
	
	
	// Use this for initialization
	void Start () {
		dragging = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		RaycastHit rayHit;
		
		
		 Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if(Physics.Raycast(ray, out rayHit)) {
			// where did the raycast hit in the world - position of rayhit
			if (dragging) {print ("rayHit.point : " + rayHit.point + " (mousePoint)");}
			
			mousePoint = rayHit.point;
	
			if (Input.GetMouseButtonDown(0)) {
				OnTouchBegin(mousePoint);
			}
			else if (Input.GetMouseButton(0)) {
				OnTouchMove(mousePoint);
			}
			else if (Input.GetMouseButtonUp(0)) {
				OnTouchEnd(mousePoint);
			}
		}
	
	}
	
	protected virtual void OnPathStart(Vector3 pointCurrent) 
    {
		if (this.PathStart != null)
			PathStart(this, pointCurrent);
    }
	
	protected virtual void OnPathEnd(Vector3 pointCurrent) 
    {
		if (PathEnd != null)
			PathEnd(this, pointCurrent);
    }
	
	protected virtual void OnPointAdded(Vector3 pointCurrent) 
    {
		if (PointAdded != null)
			PointAdded(this, pointCurrent);
    }

	private void OnTouchBegin (Vector3 pointCurrent) {
		countDrag = 0;
		points.Clear();
		AddSplinePoint(pointCurrent);
		dragging = true;
		moving = false;
		OnPathStart(pointCurrent);
	}
	
	private void OnTouchMove (Vector3 pointCurrent) {
		if ((dragging) && (countDrag < 100)) {
			print("countDrag " + countDrag);
			AddSplinePoint(pointCurrent);
			OnPointAdded(pointCurrent);
		} else {
			dragging = false;
			moving = true;
		}
		
		
	}
	
	private void OnTouchEnd (Vector3 pointCurrent) {
		dragging = false;
		moving = true;
		OnPathEnd(pointCurrent);
	}
	
	private void AddSplinePoint (Vector3 point) {
		// store co-ordinates
		points.Add(point);
		
	
		// next position
		countDrag ++;
	}
	
	 public event PathStart PathStart;
	 public event PathEnd PathEnd;
	 public event PointAdded PointAdded;
	

}
