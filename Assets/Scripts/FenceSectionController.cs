using UnityEngine;
using System.Collections;

public class FenceSectionController : MonoBehaviour {

	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	private FenchMeshHelper fenceMeshHelper;
	private MeshBuilder meshBuilder;
	public BoxCollider boxCollider;
	
	// Use this for initialization
	void Start () {
	
	  
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	float postSeperation = 10;
	float minimumPointSeperation = 5;
	float postHeight = 5;
	float postWidth = 1;
	
	public void RenderFenceSection(Vector3 point1, Vector3 point2){
		fenceMeshHelper = new FenchMeshHelper();
		meshBuilder = new MeshBuilder();
		fenceMeshHelper.BuildSection(meshBuilder,point1,point2,postSeperation,postWidth,postHeight);
		meshFilter.mesh = meshBuilder.CreateMesh();
		
		float distanceFromLastPoint = System.Math.Abs((point1 - point2).magnitude);
		
		boxCollider.size = new Vector3(distanceFromLastPoint,postHeight,postWidth);
		
	}
}
