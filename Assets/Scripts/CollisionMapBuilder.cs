using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;




public class CollisionMapBuilder : MonoBehaviour {

	public Collider terrainCollider;
	public GameObject terrainGameObject;
	
	// Use this for initialization
	void Start () {
		
		Texture2D texture = BuildMap(terrainCollider.bounds,1f);
		terrainGameObject.renderer.material.mainTexture = texture;
		terrainGameObject.renderer.material.mainTextureScale = new Vector2 (1,1);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	
	public Texture2D BuildMap(Bounds bounds, float resolution){
		
		//we need to create a texture
		//each point in the texture will represent a point in the game world
		//so clearly, the bigger the texture the more accurate the map will be 
		
		//so each pixel will actually represent a range i.e
		//pixel at the top/left of the terrain (we will assume this is only in the x/z plane)
		
		//Vector3 min = bounds.min;
		//min.Set(min.x ,0,min.z);
		
		Vector3 min = bounds.center;
		min.Set(min.x -bounds.extents.x ,0,min.z - bounds.extents.z);
		
		List<GameObject> gameObjects = FindGameObjectsWithLayer(11);
		
		List<Collider> colliders  = FindCollidersInGameObjects(gameObjects);
		
		int textureWidth = (int)System.Math.Ceiling((2*bounds.extents.x)/resolution);
		int textureHeight = (int)System.Math.Ceiling((2*bounds.extents.z)/resolution);
		
		var texture = new Texture2D(textureWidth, textureHeight);
		bool mark = false;
		for(var i=0; i<textureWidth;i++){
			for(var j=0;j<textureHeight;j++){
				
				Vector3 pos = min + new Vector3(i *resolution,0,j* resolution);
				
				mark = false;
				foreach(Collider c in  colliders){
				if(IsInside(c,pos)){
						mark = true;
						break;
					}
				}
				if(mark) {
					 texture.SetPixel(j, i, Color.red);
				} else {
				  texture.SetPixel(j, i, Color.blue);
				}
			}
		}
 
    // set the pixel values
   // texture.SetPixel(0, 0, Color(1.0, 1.0, 1.0, 0.5));
    //texture.SetPixel(1, 0, Color.clear);
    //texture.SetPixel(0, 1, Color.white);
    //texture.SetPixel(1, 1, Color.black);
 
    // Apply all SetPixel calls
    	texture.Apply();
		
		return texture;
	
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
