using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainHeightMapBuilder : MonoBehaviour {

	
	//http://www.stuffwithstuff.com/robot-frog/3d/hills/hill.html
	//http://www.regentsprep.org/regents/math/algtrig/ATT7/sinusoidal.htm
	
	
	public Terrain terrain;
	
	// Use this for initialization
	void Start () {
		
		//return;
		//step 1 Start with a flat terrain (initialize all height values to zero).
    	var nRows = terrain.terrainData.heightmapResolution;
		var nCols = terrain.terrainData.heightmapResolution;
		float noiseScale = 0.02f;
		var heights = new float[nRows, nCols];
		
		
		//var r = 50;
		//int cX = 100;
		//int cY = 100;
		
		System.Random rnd = new System.Random();
		
		for(var i=0; i< 10;i++){
		
			int cX = rnd.Next(1, nRows); 
			int cY = rnd.Next(1, nCols); 
			int r = rnd.Next(50, 100); 
			heights = RaiseHill(heights,cX,cY,r);
		}
	
		
		
   	 	//for (var j = 0; j < nRows; j++) {
       	//	for (var i = 0; i < nCols; i++) {
		 //		heights[j,i] = RaiseHill(cX,cY,r,i,j);
		//	}
		//}
		
		
		
		//heights = RaiseHill(heights,cX,cY,r);
		
		//Step 3 normalise
		//heights = Normalise(heights);
		//Flatten(heights);
		
		terrain.terrainData.SetHeights (0,0,heights);
		
		
		/*
		List<TreeInstance> treeInstances = new List<TreeInstance>(terrain.terrainData.treeInstances);
		
		TreeInstance pTI = new TreeInstance();
		
		float z = terrain.SampleHeight(new Vector3(250 , 250,100));
		
	pTI.position = new Vector3(250 , 250,z);

pTI.widthScale = 1;

pTI.heightScale = 1;

pTI.color = Color.yellow;

pTI.lightmapColor = Color.yellow;

pTI.prototypeIndex = 0; //?

//m_Terrain.AddTreeInstance(pTI);
		
		
		
		treeInstances.Add(pTI);
		
		
		
		//treeInstances = new 
		
		terrain.terrainData.treeInstances = treeInstances.ToArray();*/
	}
	
	private float[,] Flatten(float[,] heights){
		
		var nRows = terrain.terrainData.heightmapResolution;
		var nCols = terrain.terrainData.heightmapResolution;
		for (var j = 0; j < nRows; j++) {
       		for (var i = 0; i < nCols; i++) {
				heights[j,i] = heights[j,i] * heights[j,i] ;
			}
		}
		
		
		return heights;
	}
	
	private float[,]  Normalise(float[,] heights){
		float min = 0;
		float max =0;
		float height = 0;
		var nRows = terrain.terrainData.heightmapResolution;
		var nCols = terrain.terrainData.heightmapResolution;
		for (var j = 0; j < nRows; j++) {
       		for (var i = 0; i < nCols; i++) {
				height = heights[j,i];
				if(height<=min) {
					min = height;
				}
				if(height>=max) {
					max = height;
				}
			}
		}
		
		for (var j = 0; j < nRows; j++) {
       		for (var i = 0; i < nCols; i++) {
				height = heights[j,i];
				heights[j,i] = (height-min)/(max-min);
			}
		}
		return heights;
	
	}
	
	private float[,] RaiseHill(float[,] heights, int cX, int cY, int r){
		var nRows = terrain.terrainData.heightmapResolution;
		var nCols = terrain.terrainData.heightmapResolution;
		for (var j = 0; j < nRows; j++) {
       		for (var i = 0; i < nCols; i++) {
		 		heights[j,i] +=CalculateHillPoint(cX,cY,r,i,j);
			}
		}
		
		return heights;
	
	}
	
	/*
	private float CalculateHillPoint(int cX, int cY, int r, int x, int y){
		float z =  (r*r) - (((x-cX)*(x-cX)) +((y-cY)*(y-cY)));
		if(z<0) z = 0;
		return z;
	}*/
	
	private float CalculateHillPoint(int cX, int cY, int r, int x, int y){
	
		r =r*4; // we only care about 1/4 of the wav
		
		//period is r = 2pi/b
		//so b = 2pi/r
		
		float b=  (2*Mathf.PI)/r;
		
		//cX = 250;
		//cY = 250;
		
		float z =  (0.25f) * (Mathf.Cos((b)*(x-cX)) * Mathf.Cos((b)*(y-cY)));
		
		if(Mathf.Abs(x-cX)>(r/4f)) {
			z = 0;
		}
		
		if(Mathf.Abs(y-cY)>(r/4f)) {
			z = 0;
		}
		
		if(z<0f) z = 0f;
		return z;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Texture2D GenerateMap(){
	
//	}
}
