using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathRenderer : MonoBehaviour {

	public FreePathManager pathManager;
			
	public GameObject objectPathMarker;
	public IList<GameObject> objectPathMarkers = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		
		pathManager.PathStart+= new	PathStart(PathStarted);
		pathManager.PathEnd+= new PathEnd(PathEnded);
		pathManager.PointAdded+= new PointAdded(PointAdded);
	
	}
			
	void PathStarted(IPathManager manager, Vector3 point){
	
	}
	
	void PointAdded(IPathManager manager, Vector3 point){
		
		GameObject newMarker = (GameObject)Instantiate(objectPathMarker, point, transform.rotation);
		// show path : Instantiate and load position into array as gameObject
		objectPathMarkers.Add(newMarker);

	
	}
	
	void PathEnded(IPathManager manager, Vector3 point){
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
