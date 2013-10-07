using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainHeightMapBuilder : MonoBehaviour {

	
	//http://www.stuffwithstuff.com/robot-frog/3d/hills/hill.html
	//http://www.regentsprep.org/regents/math/algtrig/ATT7/sinusoidal.htm
	
	
	public Terrain terrain;
	
	private float[,] heights;
	private float[,] errosion;
	private float[,] density;
	
	// Use this for initialization
	void Start () {
		
		//return;
		//step 1 Start with a flat terrain (initialize all height values to zero).
    	var nRows = terrain.terrainData.heightmapResolution;
		var nCols = terrain.terrainData.heightmapResolution;
		float noiseScale = 0.02f;
		heights = new float[nRows, nCols];
		errosion = new float[nRows, nCols];
		density = GenerateDensity();
		
		System.Random rnd = new System.Random();
		
		for(var i=0; i< 25;i++){
		
			int cX = rnd.Next(1, nRows); 
			int cY = rnd.Next(1, nCols); 
			int r = rnd.Next(50, 100); 
			heights = RaiseHill(heights,cX,cY,r);
		}
	
		//FindMinMax(heights);
		
		
		
		
		//while(!IsOffMap(heights, nextX,nextY)){ =
		
		
		
		//	lastHeight = heights[maxXIndex,maxYIndex];
		
		//lastErodedX = maxXIndex;
		//lastErodedY = maxYIndex;
		
		//for(var k=0; k<10;k++) {
			
		//while(!IsOffMap(heights,lastErodedX,	lastErodedY)){
		//for(var k=0; k<20;k++ ){	
			
		
		/*
			Erode(maxXIndex,maxYIndex, 10,10,1,1); //pick random gradient..
				
				for(int i=0; i<nRows;i++){
					for(int j=0; j<nRows;j++){
						heights[i,j] = heights[i,j] - errosion[i,j];
					}
				}
			//}
		//}
		//}
		
		*/
		
		/*
		GenerateRiverPath();
		
		for(int i=0; i<nRows;i++){
			for(int j=0; j<nRows;j++){
				density[i,j] = density[i,j] + errosion[i,j];
			}
		}
		*/
		
		Debug.Log(numIterations);
		terrain.terrainData.SetHeights (0,0,heights);
		
		
		float[] flatHeights = TwoDimensionToOneDimension(heights);
		int[] sortedIndices = GetIndicesOfSortedArray(flatHeights);
		
		//so now we have the indices of the heights array in height order		
		//this is the sorted indices into the flatHeights
		//GetPointFrom(0
		
		
		 List<TreeInstance> newTrees = new List<TreeInstance>();
		terrain.terrainData.treeInstances = newTrees.ToArray();
		
		int index = sortedIndices[sortedIndices.Length-1];
		
		Debug.Log("index " + index + " " + flatHeights[0] );
		
		
		
		
		
		for(int j=0;j<5000;j++){
			
			int i = rnd.Next(0,100000);
			Point terrainPoint = GetPointFrom(sortedIndices[i],terrain.terrainData.heightmapResolution);
			FillRegionWithTrees(terrainPoint, newTrees, 1);
		}
		
	}
	
	public void FillRegionWithTrees(Point terrainPoint,  List<TreeInstance> newTrees, int numTrees) {
		
		//System.Random rnd = new System.Random();
		
		//Debug.Log("point " + terrainPoint.x + " " +terrainPoint.y);
		
		float treePosX = (float)(terrainPoint.x)/(float)(terrain.terrainData.heightmapResolution);
		float treePosZ = (float)(terrainPoint.y)/(float)(terrain.terrainData.heightmapResolution);
		
	//	float width = 1f/(float)(terrain.terrainData.heightmapResolution);//
		//float halfSize = width/2f;
		
	//	float minTreePosX = treePosX - halfSize;
	//	float minTreePosZ = treePosZ - halfSize;
		
	//	float maxTreePosX = treePosX + halfSize;
	//	float maxTreePosZ = treePosZ + halfSize;
		
		//Debug.Log("tree pos " + treePosX + " " + treePosZ);
		
		//float [,] treePositionMask = new float[100,100];
		//for(var i=0;i<10000;i++){
		
		//System.Random rnd = new System.Random();
		
		//bool foundEmptySpot = false;
		//int x = 0;
		//int z = 0;
		
		
		//for(var i = 0; i< numTrees;i++ ){
		
		//	while(!foundEmptySpot) {
		//		x = rnd.Next(0,100);
		//		z = rnd.Next(0,100);
				
				//Debug.Log("new x & Y");
			
		//		foundEmptySpot = treePositionMask[x,z] ==0;	
		//	}
		//	treePositionMask[x,z] = 1;
		//	foundEmptySpot = false;
			
			//Debug.Log("x&y " + x + " " + z );
			
		//	float xPos = ((float)x * (width/100f)) +  treePosX;
		//	float zPos = ((float)z * (width/100f)) +  treePosZ;
			
		//	Debug.Log("xpos & ypose " + xPos + " " + zPos );
			
			Vector3 treePosition = new Vector3(treePosX,0,treePosZ);
			CreateTree(newTrees,treePosition);
		//}
		
		
	}
	
	public Point GetPointFrom(int index, int width) {
	
		int	x = index % width;
 		int y = index / width; 
		return new Point(x,y);
	}
	
	public float[] TwoDimensionToOneDimension(float[,] map) {
		
		int width = map.GetLength(0);
		int height = map.GetLength(1);
		float[] output = new float[width*height];
		for(int i = 0; i < height; i++){
		  for(int j = 0; j < width; j++){
		    output[i*height+j] = map[i,j];
		  }
		}
		
		return output;
	}
	
	
	public int[] GetIndicesOfSortedArray(float[] input) {
		int[] indices = new int[input.Length];
		for (int i = 0; i < indices.Length; i++) indices[i] = i;
		System.Array.Sort(input, indices);
		return indices;
	}
	
//	public Vector3 PickRandomPositionOnMap(){
	//}
	

	public Vector3 PickTreePosition(){
		
	//	PointToPixel(Ra
	
		//Random gives value between 0 and 1
		
		
		float treeX = 0;
		float treeZ = 0;
		int x;
		int y;
		float height = 0.0f;
		
		while(height<0.5f) {
			treeX = Random.value;
			treeZ = Random.value;
			
			x = (int)(treeX * terrain.terrainData.heightmapResolution);
			y = (int)(treeZ * terrain.terrainData.heightmapResolution);
			Debug.Log(treeX + " " + treeZ + " " + x + " " + y);
			height  = heights[x,y];
		}
		
		return new Vector3 (treeX, 0, treeZ);
		
	}
	
	
	public void CreateTree( List<TreeInstance> treeInstances, Vector3 treePosition){
		
		TreeInstance tempInstance = new TreeInstance ();
		
		tempInstance.prototypeIndex = 0;
		
		tempInstance.color = Color.white;
		
		tempInstance.heightScale = 2;
		
		tempInstance.widthScale = 2;
		tempInstance.lightmapColor = Color.white;
		
		tempInstance.position = treePosition;
		
		terrain.AddTreeInstance(tempInstance);
		
		
	}
	
	
	//private Vector2 FindMinimu
	
	private List<float> riverPath = new List<float>(); 
	
	private int numIterations = 0;
	private int nextX = 1;
	private int nextY = 1;
	//private int lastX = 1;
//	private int
	
	private List<Vector2> navigatedPoints = new List<Vector2>();
	
	
	public float[,] GenerateDensity()
    {
        float[,] density = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
 
        for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
            {
                density[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapResolution)*5f, ((float)k / (float)terrain.terrainData.heightmapResolution)* 5f );
            }
        }
 
        return density;
    }
	
	//private List<float> // = new List<float>();
	
	public struct Point
	{
	    public int x;
	    public int y;
		
		public Point(int x, int y){
			this.x = x;
			this.y = y;
		}
	}
	
	
	private void GenerateRiverPath(){
	
	
		//1 Lets find an edge
		System.Random rnd = new System.Random();
		int side = rnd.Next(0,3);
		
		//    0
		//    ---
		//  3|	| 1
		//   ----
	    //    2
		
		int max = terrain.terrainData.heightmapResolution-1;
		int start = rnd.Next(0,max);
		
		int x;
		int y;
		int gradX;
		int gradY;
		
		if(side ==0 ){
		 	x = start;	
			y = 0;
		} else if (side == 1) {
			x = max;	
			y = start;
		} else if(side ==2) {
			x = start;
			y = max;	
		} else {
			x = 0;
			y = start;	
		}
		
		Debug.Log("start " + x + " " + y);
		
		Point point = new Point(x,y);
		
		//Point nextPoint ;
		while(!IsOffMap(heights,point)){
			density[point.x,point.y]=1000;
			numIterations = numIterations+1;
			point = CreateNextPoint(point);
			errosion[point.x,point.y] = 10;
			
			
			if(numIterations>1000) {
				break;
			}
			
		}
		
	}
	
	const float OFF_MAP = 1000000;
	
	public Point CreateNextPoint(Point currentPoint) {
		
		float[,] sample = SampleFrom2DArray(density,currentPoint,1);
		 //force current point to be ignored
		Point lowestPoint = FindLowestPoint(sample); // lowest point in the sample	
		
		
		
		int nextX = currentPoint.x + lowestPoint.x -1;
		int nextY = currentPoint.y + lowestPoint.y -1;
		return new Point(nextX,nextY);
	}
	
	/*
	public bool CreatePath(Point currentPoint) {
		numIterations = numIterations +1;
		if(numIterations>513) {
			return;
		}
		
		if( IsOffMap(heights,currentPoint)) {
			Debug.Log("off");	
				return;
		}
		//Debug.Log("not off");
		errosion[currentPoint.x,currentPoint.y] = 10;
		
		float[,] sample = SampleFrom2DArray(density,currentPoint,1);
		
		
		
		Point lowestPoint = FindLowestPoint(sample); // lowest point in the sample	
		
		//Debug.Log ("lowestPoint " + lowestPoint.x + " " + lowestPoint.y);
		
		int nextX = currentPoint.x + lowestPoint.x -1;
		int nextY = currentPoint.y + lowestPoint.y -1;
		
		Debug.Log ("next " + nextX + " " + nextY);
		
		if(currentPoint.x ==nextX && currentPoint.y==nextY){
			Debug.Log("end");
			return;
		} else {
			return new Point(nextX,nextY);
		}
		
		//CreatePath(new Point(nextX,nextY));
	
	}
	*/
	public bool IsOffMap(float[,] map, Point point) {
		return (point.x<0 || point.x > (map.GetLength(0)-1) || point.y<0 || point.y>(map.GetLength(1)-1));
	}
	
	
	public Point FindLowestPoint(float [,] map){
		
	
		//Debug.Log("start FindLowestPoint");
		
	
		Point minPoint = new Point(0,0);
		float minValue = 1000;
		
		
			
		for(int i = 0; i<map.GetLength(0);i++){
			
			for(int j = 0; j<map.GetLength(1);j++){
		//		Debug.Log("lowest value " +  map[i,j] + " " + i + " " + j);
				if(map[i,j]<=minValue){
					
					
					
					minValue = map[i,j];
					minPoint = new Point(i,j);
					
					//Debug.Log("min value " + minValue);
					//Debug.Log(minValue);
					//minPoint.x = i;
					//minPoint.y = j;
				}
			}
			
		}
		
		Debug.Log("end FindLowestPoint");
		
		return minPoint;
		
	}
	
	
	public float[,] SampleFrom2DArray(float[,] map, Point point, int halfSize){
	
		
		int minX = point.x - halfSize;
		int maxX = point.x + halfSize;
		int minY = point.y - halfSize;
		int maxY = point.y + halfSize;
		
		float [,] sample = new float[(2*halfSize) + 1,(2*halfSize) + 1];
	
		//Debug.Log("Sample from" + minX + "to " + maxX );
		
		for(int i = minX; i<=maxX;i++){
			
			for(int j = minY; j<=maxY;j++){
				
				float val = GetPoint(map,i,j,OFF_MAP);
				sample[i-minX,j-minY] = val;
		//		Debug.Log("Sample " + (i-minX) + " " + (j-minY) + " "+ val );
			}
			
		}
		
		return sample;
	
	
	}
	
	public float GetPoint(float[,] map, int x, int y, float offMapValue){
		if(x<0 || x > (map.GetLength(0)-1) || y<0 || y>(map.GetLength(1)-1)){
		//	Debug.Log("CUNT");
			return offMapValue;
		} else {
		//	Debug.Log("NOT CUNT" + x + " " + y + " " + map[x,y]);
			return map[x,y];
		}
	}
			
	public Point NormalisePoint(float[,] map,Point point){
	
		if(point.x<0){
			point.x = 0;
		}
		
		if(point.x>map.GetLength(0)){
			point.x = map.GetLength(0)-1;
		}
		
		if(point.y<0) {
			point.y = 0;
		}
		
		if(point.y>map.GetLength(1)){
			point.y = map.GetLength(1)-1;
		}
		
		return point;
			
			//if(pointx<0 || x > (heights.GetLength(0)-1) || y<0 || y>(heights.GetLength(1)-1));
		
	}
	
	/*
	public void ErrodePath(Vector2 force, int currentX, int currentY){
		
		//float[] surroundings = FindSurroundingPoints(density, currentX, currentY);
		int lowestIndex = FindLowestPointsOfSurrounding(surroundings);
		//we'll add a force to force which moves the river in that direction
		
		int y = lowestIndex/3;
		int x = lowestIndex - 3*y;
				
		int nextY = (startY -1)	+ y; //points to erode
		int nextX = (startX -1)	+ x; // points to erode
		
		
		
		
		//calculate next point
		
		//is next point off map?
		if( IsOffMap(heights,startX,startY)) return;
	
	
	}
	
	//private Vector2 CalculateForce(Vector2 
	
	//public void CalculateVector(int x1, int y1, int x2, int y2){
		//x2-x1;
		
	
	//}
	
		
	const float OFF_MAP = 1000000;
	
	public bool IsOffMap(float[,] heights, int x, int y) {
		return (x<0 || x > (heights.GetLength(0)-1) || y<0 || y>(heights.GetLength(1)-1));
	}
	
	
	
	
	
	//continue on current gradient unless energy is 0.
	//if energy is zero. or less.find lowest point
	
	
	private float lastHeight = 10000000f;
	private int lastErodedX = 0;
	private int lastErodedY = 0;
	
	//set volume of water.
	//errode a block at the current point. Can volume fit through
	//then water would overspill. erode points either side (accounting for density)
	//can water fit through? if so, move to next point
	
	//energy allows flowing uphill...
	
	
	
	//the more the water there is 
	
	private void Erode(int startX, int startY, float volume, float energy, int gradX, int gradY) {
		
		numIterations = numIterations +1;
		
		if( IsOffMap(heights,startX,startY)) return;
		
		//update the energy...
		//a droplet loses energy by gaining heigth and eroding
		//a droplet gains height by losing height.
		float currentHeight = heights[startX,startY]; //whats the current height?
		energy-=0.1f; //some random energy loss
		energy += ( lastHeight - currentHeight); //gains/looses energy based upon gravity. and amount of errosion perfomed.
		
		if(energy<0){
			energy = 0;
		}
		
		if(energy>10){
			Debug.Log("too much energy");
			energy = 10;
		}
		
		//calculate the amount of erosion
		float amountOfErosion = 0.05f * (1f/density[startX,startY]) * energy; //amount of errosion is inversely proportional to density and proportional to energy
		
		//reduce energy by amount proportional to erosion.
		//energy = energy - amountOfErosion;
		
		
		
		//update erosion map.
		errosion[startX,startY] = +amountOfErosion;
		
		lastErodedY = startY;
		lastErodedX = startY;
	
		//model water absorption...
		volume = volume - (0.1f);  
		
		if(volume<0) { 
		//	Debug.Log("run out of water");
		//	return; //all the water has been obsorbed...
		}
		
		//lets now calulate the next point to erode. At this point all eroded points are stored in navigatedPoints
		//this allows us to calculate a gradient
		
		if(energy==0) {
			Debug.Log("run out of energy");
			//return;
			
			//our droplet has run of energy.
			
			//pick lowest nearby point.
			
			//sample all the points around this point
			//0 1 2
			//3 4 5
			//6 7 8
			
			float[] surroundings = FindSurroundingPoints(heights,startX,startY);
			int lowestIndex = GetIndexOfLowestPoint(surroundings);
			
			
			if(lowestIndex==4) {
				//not sure what to do yet
				Debug.Log("wah");
				return;
			}
			
			if(surroundings[lowestIndex]==OFF_MAP){
				return;
			}
			
			int y = lowestIndex/3;
			int x = lowestIndex - 3*y;
				
			nextY = (startY -1)	+ y; //points to erode
			nextX = (startX -1)	+ x; // points to erode
			
			gradX = x;
			gradY = y;
			
			energy = 0;

			//clear the navigated points since previous points are unrelated to this new direction.
			
		//	navigatedPoints.Clear();
			//navigatedPoints.Add(new Vector2(startX,startY));
			//navigatedPoints.Add(new Vector2(nextX,nextY));
			//now we should be able to generate a gradient.

			
		
		} else {
			
			nextX = startX + gradX;
			nextY = startY + gradY;		
		}
		
		//set the height
		lastHeight = heights[startX,startY];
		
		//add to our list the points we've just navigated.
		
		
		Erode(nextX,nextY, volume, energy, gradX, gradY);
		
		
		//if all the water cant fit in the brush volume.
		//we could start another droplet in adjacent points
		
		
		//brush = errosion[startX,startY];
		
	//	if(errosion[startX,startY]<volume){
	//		//reduce either side.
	//	}
		
		
		//int nextY;
		//int nextX;
		
		//if(lowestIndex==4) {
			
			
			//if energy > 0 we can move up hill in our current direction
			
			
			
			//Debug.Log("FLAT OR LOWEST POINT");
			//our current point is the local minima
			//we need to do some digging.
			
			//lets continue the current gradient
			
			//reduce the volume of water
			
			//increase height here of middle point?
			//errosion[startX,startY] = -0.05f;
			
			//vol
			
			//here we decide based upon the density map
			
			
		//} else {
		
	//		if(surroundings[lowestIndex]==OFF_MAP){
	//			return;
	//		}
		
	//	if(
			
	//		lastHeight = heights[startX,startY];
	//	
	//		int y = lowestIndex/3;
	//		int x = lowestIndex - 3*y;
				
	//		nextY = (startY -1)	+ y;
	//		nextX = (startX -1)	+ x;
			
		//	Debug.Log(lowestIndex + " " + nextX + " " + nextY);
			
	//		Erode(nextX,nextY, volume);
		//}
		
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
	
	*/
	
	
	
	
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
