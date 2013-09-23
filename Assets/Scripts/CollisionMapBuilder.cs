using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;



public class CollisionMapBuilder : MonoBehaviour {

	public Collider terrainCollider;
	public GameObject terrainGameObject;
	public GameObject prefab;
	
	// Use this for initialization
	void Start () {
		
		BuildMap();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float MapResolution{get;set;}
	public Bounds WorldBounds{get;set;}
	public Vector2 MapSize{get;set;}
	private int textureHeight;
	private int textureWidth;
	
	//public float MapResolution {get;set;}
	
	public void BuildMap(){
		
			Texture2D texture = BuildMap(terrainCollider.bounds,1f);
		//terrainGameObject.renderer.material.
		terrainGameObject.renderer.material.mainTexture = texture;
		terrainGameObject.renderer.material.mainTextureScale = new Vector2 (1,1);
	
	}
	
	
	public Texture2D BuildMap(Bounds bounds, float resolution){
		
		//we need to create a texture
		//each point in the texture will represent a point in the game world
		//so clearly, the bigger the texture the more accurate the map will be 
		
		//so each pixel will actually represent a range i.e
		//pixel at the top/left of the terrain (we will assume this is only in the x/z plane)
		
		//Vector3 min = bounds.min;
		//min.Set(min.x ,0,min.z);
		this.MapResolution = resolution;
		this.WorldBounds = bounds;
		
		//Vector3 min = bounds.center;
		//min.Set(min.x -bounds.extents.x ,0,min.z - bounds.extents.z);
		
		List<GameObject> gameObjects = FindGameObjectsWithLayer(11);
		
		//List<Collider> colliders  = FindCollidersInGameObjects(gameObjects);
		
		 textureWidth = (int)System.Math.Ceiling((2*bounds.extents.x)/resolution);
		textureHeight = (int)System.Math.Ceiling((2*bounds.extents.z)/resolution);
		
		var texture = new Texture2D(textureWidth, textureHeight);
		
		foreach(GameObject c in gameObjects){
			RasteriseCollider(c,texture);
		}
		
    	// Apply all SetPixel calls
    	texture.Apply();
		
		return texture;
	
	}
	
	public struct Pixel {
		public Pixel(int x, int y){
			this.X = x;
			this.Y=y;
		}
		
		public int X {
	        get;set;
	    }
		public int Y {
	        get;set;
	    }
	}
	
	public Pixel ConvertFromWorldSpaceToMapSpace(Vector2 point) {
		
		//first lets change the co-ordinate system
		
	//	Vector2 min = new Vector2(WorldBounds.center.x + WorldBounds.extents.x ,WorldBounds.center.z+ WorldBounds.extents.z);
		//min.Set(min.x + WorldBounds.extents.x ,min.y + WorldBounds.extents.z);
		
		
		
		
		Vector2 translatedPoint = point + new Vector2 (WorldBounds.center.x,WorldBounds.center.z); // + new Vector2( WorldBounds.extents.x,  WorldBounds.extents.z) - new Vector2 (WorldBounds.center.x,WorldBounds.center.z);
		
		
		double x = System.Math.Ceiling(point.x/MapResolution) + (textureWidth/2f); //wtf this isnt offsetting properly?
		double y = System.Math.Ceiling(point.y/MapResolution) + (textureHeight/2f);
		
		return new Pixel((int)x,(int)y);
	}
	
	
	public List<Vector3> GetColliderVertexPositions (GameObject gameObject )  {

	    List<Vector3> vertices = new List<Vector3>();
	
	    var thisMatrix = gameObject.transform.localToWorldMatrix;
	
	    var storedRotation = gameObject.transform.rotation;
		
	    gameObject.transform.rotation = Quaternion.identity;
	
	    var extents = gameObject.collider.bounds.extents;
	
	    //vertices.Add (thisMatrix.MultiplyPoint3x4(extents));
	
	    //vertices.Add (thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z)));
	
	    //vertices.Add (thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z)));
	
	   // vertices.Add (thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z)));
		
		
	
		vertices.Add (thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z)));

		vertices.Add(thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z)));
	
	
	    vertices.Add(thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, extents.z)));
	
	    
	    vertices.Add (thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z)));
	
		Debug.Log("rotated");
		 Debug.Log("center " + gameObject.collider.bounds.center.x + " " + gameObject.collider.bounds.center.z);
	    Debug.Log(vertices[0].x + " " + vertices[0].z);

		Debug.Log(vertices[1].x + " " + vertices[1].z);
		Debug.Log(vertices[2].x + " " + vertices[2].z);
		Debug.Log(vertices[3].x + " " + vertices[3].z);
		
		gameObject.transform.rotation = storedRotation;
	
	    return vertices;
	
	}
	
	public void RasteriseCollider(GameObject collider, Texture2D texture){
	
		
		if(collider.collider!=null) {
		
			
			List<Vector3> colliderPositions = GetColliderVertexPositions(collider);
			
			//Vector2 point1 = new Vector2(
		// .point2      .point3 
		//
		//
		// .point1      .point4
			
			//collider.collider.bounds.size
		
			
			//problem with big objects is bounds related
			//use collider.collider.bounds.size instead?
	  	Vector2 point1 = new Vector2(collider.collider.bounds.min.x,collider.collider.bounds.min.z);
		Vector2 point2 = new Vector2(collider.collider.bounds.min.x,collider.collider.bounds.max.z);
		Vector2 point3 = new Vector2(collider.collider.bounds.max.x,collider.collider.bounds.max.z);
		Vector2 point4 = new Vector2(collider.collider.bounds.max.x,collider.collider.bounds.min.z);
			
			
			Debug.Log("BOX COLLIDER SIZE " + collider.collider.bounds.size.x + " " + collider.collider.bounds.size.y +  " " + collider.collider.bounds.size.z);
			
			
		
		//	Debug.Log(point1.x + " " + point1.y);

		//Debug.Log(point2.x + " " + point2.y);
		//Debug.Log(point3.x + " " + point3.y);
		//Debug.Log(point4.x + " " + point4.y);
			
			
	//	point1 = rotatePoint(collider.collider.bounds.center.x,collider.collider.bounds.center.z,collider.transform.rotation.y, point1);
	//	point2 = rotatePoint(collider.collider.bounds.center.x,collider.collider.bounds.center.z,collider.transform.rotation.y, point2);
	//	point3 = rotatePoint(collider.collider.bounds.center.x,collider.collider.bounds.center.z,collider.transform.rotation.y, point3);
	//	point4 = rotatePoint(collider.collider.bounds.center.x,collider.collider.bounds.center.z,collider.transform.rotation.y, point4);
		
		
		//	Debug.Log("NOT rotated");
		
	    //Debug.Log(point1.x + " " + point1.y);

	//	Debug.Log(point2.x + " " + point2.y);
	//	Debug.Log(point3.x + " " + point3.y);
	//	Debug.Log(point4.x + " " + point4.y);
	
	//		Pixel pixel1 = ConvertFromWorldSpaceToMapSpace(point1);
			//Pixel pixel2 = ConvertFromWorldSpaceToMapSpace(point2);
			
	//		Pixel pixel3 = ConvertFromWorldSpaceToMapSpace(point3);
	//		Pixel pixel4 = ConvertFromWorldSpaceToMapSpace(point4);
			
			
		//our triangles are point1, point2, point3 and point1, point3, point4
		
		//We get the nearest pixel for each of these points
		Pixel pixel1 = ConvertFromWorldSpaceToMapSpace(new Vector2(colliderPositions[0].x,colliderPositions[0].z));
		Pixel pixel2 = ConvertFromWorldSpaceToMapSpace(new Vector2(colliderPositions[1].x,colliderPositions[1].z));
		Pixel pixel3 = ConvertFromWorldSpaceToMapSpace(new Vector2(colliderPositions[2].x,colliderPositions[2].z));
		Pixel pixel4 = ConvertFromWorldSpaceToMapSpace(new Vector2(colliderPositions[3].x,colliderPositions[3].z));
			
			
		//	Instantiate(prefab,colliderPositions[0],this.transform.rotation);
//			Instantiate(prefab,colliderPositions[1],this.transform.rotation);
			//Instantiate(prefab,colliderPositions[2],this.transform.rotation);
			//Instantiate(prefab,colliderPositions[3],this.transform.rotation);
			
		
		IEnumerable<Pixel> pointsInTriangle1 = PointsInTriangle(pixel1,pixel2,pixel3);
		DrawPixelsToTexture(pointsInTriangle1,texture);
		IEnumerable<Pixel> pointsInTriangle2 = PointsInTriangle(pixel1,pixel3,pixel4);
		DrawPixelsToTexture(pointsInTriangle2,texture);
		}
		
	}
	
	private Vector2 rotatePoint(float cx,float cy,float angle,Vector2 p)
	{
	
	  //Vector2 newPoint = new Vector2(
		
	 
			
	  float s = Mathf.Sin(angle);
	  float c = Mathf.Cos(angle);
	
	  // translate point back to origin:
	 // p.x -= cx;
	 // p.y -= cy;
	  float x = p.x	- cx;	
	  float y = p.y	- cy;	
	
	  // rotate point
	  float xnew = x * c - y * s;
	  float ynew = x * s + y * c;
	
	  // translate point back:
	  //p.x = xnew + cx;
	  //p.y = ynew + cy;
	  return new Vector2(xnew+cx,ynew+cy);
	}
	
	
	
	public void DrawPixelsToTexture(IEnumerable<Pixel> pixels, Texture2D texture){
		
		foreach(Pixel p in pixels){
			 texture.SetPixel(p.X, p.Y, Color.red);
		}
	
	}
	
	
	
	public IEnumerable<Pixel> PointsInTriangle(Pixel pt1, Pixel pt2, Pixel pt3)
	{
    	if (pt1.Y == pt2.Y && pt1.Y == pt3.Y)
    	{
        	throw new ArgumentException("The given points must form a triangle.");
    	}

	    Pixel tmp;
	
	    if (pt2.X < pt1.X)
	    {
	        tmp = pt1;
	        pt1 = pt2;
	        pt2 = tmp;
	    }
	
	    if (pt3.X < pt2.X)
	    {
	        tmp = pt2;
	        pt2 = pt3;
	        pt3 = tmp;
	
	        if (pt2.X < pt1.X)
	        {
	            tmp = pt1;
	            pt1 = pt2;
	            pt2 = tmp;
	        }
	    }
	
	    var baseFunc = CreateFunc(pt1, pt3);
	    var line1Func = pt1.X == pt2.X ? (x => pt2.Y) : CreateFunc(pt1, pt2);
	
	    for (var x = pt1.X; x < pt2.X; x++)
	    {
	        int maxY;
	        int minY = GetRange(line1Func(x), baseFunc(x), out maxY);
	
	        for (var y = minY; y <= maxY; y++)
	        {
	            yield return new Pixel(x, y);
	        }
	    }
	
	    var line2Func = pt2.X == pt3.X ? (x => pt2.Y) : CreateFunc(pt2, pt3);
	
	    for (var x = pt2.X; x <= pt3.X; x++)
	    {
	        int maxY;
	        int minY = GetRange(line2Func(x), baseFunc(x), out maxY);
	
	        for (var y = minY; y <= maxY; y++)
	        {
	            yield return new Pixel(x, y);
	        }
	    }
	}

	private int GetRange(double y1, double y2, out int maxY)
	{
	    if (y1 < y2)
	    {
	        maxY = (int)Math.Floor(y2);
	        return (int)Math.Ceiling(y1);
	    }
	
	    maxY = (int)Math.Floor(y1);
	    return (int)Math.Ceiling(y2);
	}
	
	private Func<int, double> CreateFunc(Pixel pt1, Pixel pt2)
	{
	    var y0 = pt1.Y;
	
	    if (y0 == pt2.Y)
	    {
	        return x => y0;
	    }
	
	    var m = (double)(pt2.Y - y0) / (pt2.X - pt1.X);
	
	    return x => m * (x - pt1.X) + y0;
	}
	
	public bool IsInside ( Collider test, Vector3 point)
	{
	   Vector3    center;
	   Vector3    direction;
	   Ray        ray;
	   RaycastHit hitInfo;
	   bool       hit;
	 
	   // Use collider bounds to get the center of the collider. May be inaccurate
	   // for some colliders (i.e. MeshCollider with a 'plane' mesh)
	   center = test.bounds.center;
	 
	   // Cast a ray from point to center
	   direction = center - point;
	   ray = new Ray(point, direction);
	   hit = test.Raycast(ray, out hitInfo, direction.magnitude);
	 
	   // If we hit the collider, point is outside. So we return !hit
	   return !hit;
	}
	
	
	public List<GameObject> FindGameObjectsWithLayer (int layer) {
		GameObject[] goArray = (GameObject[])FindObjectsOfType(typeof(GameObject));
		return goArray.Where(go => go.layer==layer).ToList();
	}
	
	public List<Collider> FindCollidersInGameObjects(List<GameObject> gameObjects){
		
		List<Collider> colliders = new List<Collider>();
		foreach(GameObject gameObject in gameObjects){
			colliders.Add(gameObject.GetComponent<Collider>());
		}
		
		return colliders;
	}
}
