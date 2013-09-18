using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FenceController : MonoBehaviour {

	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private FenchMeshHelper fenceMeshHelper;
	private MeshBuilder meshBuilder;
	public FenceSectionController fenceSectionPrefab;
	
	
	// Use this for initialization
	void Start () {
	    fenceMeshHelper = new FenchMeshHelper();
		meshFilter = GetComponent<MeshFilter>();
	 	meshRenderer = GetComponent<MeshRenderer>();
		meshBuilder = new MeshBuilder();
	}
	
	public void SetFenceMesh(Mesh mesh){
		meshFilter.mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	float postSeperation = 10;
	float minimumPointSeperation = 5;
	float postHeight = 5;
	float postWidth = 1;
	
	public void RenderFence(IList<Vector3> points){
		
		meshBuilder = new MeshBuilder();
		fenceMeshHelper.BuildFence(meshBuilder,points, postSeperation,postWidth,postHeight);	
		this.SetFenceMesh(meshBuilder.CreateMesh());
	}
	
	public void CompleteFence(IList<Vector3> points){
		
		//this.RenderFence(points);
		this.SetFenceMesh(new Mesh());
		//now we need a collider for each of these fence sections
		//we'll use box colliders
		
		/*
		for(var i=0; i< points.Count;i++) {
		
			Vector3 fenceSectionPosition = this.transform.position + points[i];
			FenceSectionController fenceSectionController = (FenceSectionController)Instantiate(fenceSectionPrefab,fenceSectionPosition,transform.rotation);
			fenceSectionController.RenderFenceSection(new Vector3(0,0,0),points[i+1]-points[i]);
		}*/
		
		for(var i=0; i< points.Count;i++) {
		
			Vector3 fromPoint = points[i];
			Vector3 toPoint = points[i+1];
			
			float distanceFromLastPoint = System.Math.Abs((fromPoint - toPoint).magnitude);
				
			Vector3 directionUnitVector = (fromPoint - toPoint).normalized;
	
			Vector3 midPoint = (distanceFromLastPoint/2) * directionUnitVector;
			
			Vector3 fenceSectionPosition = this.transform.position + fromPoint - midPoint ;//- midPoint;
			Vector3 right = new Vector3(1,0,0);
		
			float angle = Vector3.Angle(right, directionUnitVector);
			Vector3 cross = Vector3.Cross(right, directionUnitVector);
			 
			if (cross.y > 0)
			    angle = 360 - angle;
			
			FenceSectionController fenceSectionController = (FenceSectionController)Instantiate(fenceSectionPrefab,fenceSectionPosition,transform.rotation);
			
			fenceSectionController.transform.Rotate(new Vector3(0,-angle,0));
			
			fenceSectionController.RenderFenceSection(new Vector3(-(distanceFromLastPoint/2),0,0),new Vector3((distanceFromLastPoint/2),0,0));
		}
		
		
	}
	
	
	
	
}
