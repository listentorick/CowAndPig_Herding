using UnityEngine;
using System.Collections;

public class BasePathManager : MonoBehaviour, IPathManager {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected void OnFirstTouch(Vector3 touchPoint){
		AddPathPoint(touchPoint);
		OnPathStart(touchPoint);
	}
	
	protected void OnTouch(Vector3 touchPoint){
		AddPathPoint(touchPoint);
		OnPointAdded(touchPoint);
	}
	
	protected void OnTouchEnd(Vector3 touchPoint){
		AddPathPoint(touchPoint);
		OnPathEnd(touchPoint);
	}
	
	protected void OnTouchCancel(){
		OnPathCancelled();
	}
	
	protected void AddPathPoint(Vector3 touchPoint){
	
	}
	
	protected virtual void OnPathStart(Vector3 touchPoint) 
    {
		if (this.PathStart != null)
			PathStart(this, touchPoint);
    }
	
	protected virtual void OnPathEnd(Vector3 touchPoint) 
    {
		if (PathEnd != null)
			PathEnd(this, touchPoint);
    }
	
	protected virtual void OnPointAdded(Vector3 touchPoint) 
    {
		if (PointAdded != null)
			PointAdded(this, touchPoint);
    }
	
	protected virtual void OnPathCancelled(){
		if(PointCancelled!=null){
			PointCancelled(this);
		}
	}
	
	public event PathStart PathStart;
	public event PathEnd PathEnd;
	public event PointAdded PointAdded;
	public event PointCancelled PointCancelled;
}
