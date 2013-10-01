using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainHeightMapBuilder : MonoBehaviour {

	
	//http://www.stuffwithstuff.com/robot-frog/3d/hills/hill.html
	//http://www.regentsprep.org/regents/math/algtrig/ATT7/sinusoidal.htm
	
	
	public Terrain terrain;
	
	private float[,] heights;
	private float[,] errosion;
	
	// Use this for initialization
	void Start () {
		
		//return;
		//step 1 Start with a flat terrain (initialize all height values to zero).
    	var nRows = terrain.terrainData.heightmapResolution;
		var nCols = terrain.terrainData.heightmapResolution;
		float noiseScale = 0.02f;
		heights = new float[nRows, nCols];
		errosion = new float[nRows, nCols];
		
		//var r = 50;
		//int cX = 100;
		//int cY = 100;
		
		System.Random rnd = new System.Random();
		
		for(var i=0; i< 25;i++){
		
			int cX = rnd.Next(1, nRows); 
			int cY = rnd.Next(1, nCols); 
			int r = rnd.Next(50, 100); 
			heights = RaiseHill(heights,cX,cY,r);
		}
	
		FindMinMax(heights);
		
		Erode(maxXIndex,maxYIndex);
		
		for(int i=0; i<nRows;i++){
			for(int j=0; j<nRows;j++){
				heights[i,j] = heights[i,j] - errosion[i,j];
			}
		}
		
		
		Debug.Log(numIterations);
		terrain.terrainData.SetHeights (0,0,heights);
		
	}
	
	
	private List<float> riverPath = new List<float>(); 
	
	private int numIterations = 0;
	
	private void Erode(int startX, int startY) {
		
		numIterations =	numIterations + 1;
		
		//sample all the points around this point
		//0 1 2
		//3 4 5
		//6 7 8
		
		float[] surroundings = FindSurroundingPoints(heights,startX,startY);
		
		int lowestIndex = GetIndexOfLowestPoint(surroundings);
		
		//lowestIndex = 1;
		errosion[startX,startY] = 1f;
		
		int nextY;
		int nextX;
		
		if(lowestIndex==4) {
			
			Debug.Log("FLAT OR LOWEST POINT");
			//our current point is the local minima
			//we need to do some digging.
			
			//lets continue the current gradient
		} else {
		
			if(surroundings[lowestIndex]==OFF_MAP){
				return;
			}
			
			int y = lowestIndex/3;
			int x = lowestIndex - 3*y;
				
			nextY = (startY -1)	+ y;
			nextX = (startX -1)	+ x;
			
		//	Debug.Log(lowestIndex + " " + nextX + " " + nextY);
			
			Erode(nextX,nextY);
		}
		
		//if(nextX<0 || 
		
		
		//Erode (heights,nextX,nextY);
			
			
		
		
	
	}
	
	//private void ErodeNextPoint() {
	//}
	

	const float EVEN_GRADIENT = 2000000;
	
	public int GetIndexOfLowestPoint(float[] surroundings){
		//if we return -1 all the points have the same value.
		int minIndex = 4;
		float minValue = surroundings[4]; //this is the value of our current point (which is surrounded)
		for(int i=0; i<surroundings.Length;i++){
			if(surroundings[i]<=	minValue){
				minValue = surroundings[i];
				minIndex = i;
			}
		}
				
		return minIndex;
	}
	
	public float[] FindSurroundingPoints(float[,] heights, int startX, int startY){
		
		float[] surroundings = new float[9];
		
		//0,1,2
		//3,4,5
		//6,7,8
		
		//where 4 is startX and startY
		
		surroundings[0] = GetPoint(heights, startX-1,startY-1); //0
		surroundings[1] = GetPoint(heights, startX,startY-1); //1
		surroundings[2] = GetPoint(heights, startX+1,startY-1); //2
		
		surroundings[3] = GetPoint(heights, startX-1,startY); //3
		surroundings[4] = GetPoint(heights, startX,startY); //4
		surroundings[5] = GetPoint(heights, startX+1,startY); //5
		
		surroundings[6] = GetPoint(heights, startX-1,startY+1); //6
		surroundings[7] = GetPoint(heights, startX,startY+1); //5
		surroundings[8] = GetPoint(heights, startX+1,startY+1); //8
		

		return surroundings;
	
	}
	
	
		
	const float OFF_MAP = 1000000;
	
	public float GetPoint(float[,] heights, int x, int y){
		if(x<0 || x > (heights.GetLength(0)-1) || y<0 || y>(heights.GetLength(1)-1)){
			return OFF_MAP;
		} else {
			return heights[x,y];
		}
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
	
	//private 
	
	private float min;
	private int maxXIndex;
	private int maxYIndex;
	private float max;
	
	private void FindMinMax(float[,] heights){
		//float min = 0;
		//float max =0;
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
					maxXIndex = j;
						maxYIndex = i;
				}
			}
		}
	
	}
	
	private float[,]  Normalise(float[,] heights){
	/*
		//	float min = 0;
	//	float max =0;
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
		}*/
		
		float height = 0;
		var nRows = terrain.terrainData.heightmapResolution;
		var nCols = terrain.terrainData.heightmapResolution;
		
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
