using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FenceBuilder : MonoBehaviour {

	public BasePathManager pathManager;
			
	public FenceController fencePrefab;
	public IList<Vector3> posts;
	private MeshBuilder meshBuilder;
	private FenchMeshHelper fenceMeshHelper;
	private FenceController fenceInstance;

	// Use this for initialization
	void Start () {
		fenceMeshHelper = new FenchMeshHelper();
		pathManager.PathStart+= new	PathStart(PathStarted);
		pathManager.PathEnd+= new PathEnd(PathEnded);
		pathManager.PointAdded+= new PointAdded(PointAdded);
		pathManager.PointCancelled+=new PointCancelled(PointCancelled);
	
	}
			
	void PathStarted(IPathManager manager, Vector3 point){
		meshBuilder = new MeshBuilder();
		posts = new List<Vector3>();
		posts.Add(new Vector3(0,0,0));
		fenceInstance = (FenceController)Instantiate(fencePrefab,point,transform.rotation);
		//fenceInstance.RenderFence(posts);
	}
	
	Vector3 GetPointRelativeToFence(Vector3 point){
		return point-fenceInstance.transform.position;
	
	}
	
	void PointAdded(IPathManager manager, Vector3 point){
		
		Vector3 relativePoint = GetPointRelativeToFence(point); 
			
		posts.Add(relativePoint);
		
		fenceInstance.RenderFence(posts);
		
	}
	
	void PathEnded(IPathManager manager, Vector3 point){
		
		Vector3 relativePoint = GetPointRelativeToFence(point); 
			
		posts.Add(relativePoint);
	
		fenceInstance.CompleteFence(posts);
		
	}
		
	void PointCancelled(IPathManager manager){
		//((GameObject)fenceInstance).SetActive(false);
		//fenceInstance.Destroy();
		posts.Clear();

	}
	
	// Update is called once per frame
	void Update () {
		
		
	
	}
}
