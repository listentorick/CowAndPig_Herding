using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FenchMeshHelper : MeshHelper {

	public void BuildPost(MeshBuilder meshBuilder, Vector3 position, float postHeight, float postWidth, Quaternion rotation)
	{
		Vector3 upDir = rotation * Vector3.up * postHeight;
		Vector3 rightDir = rotation * Vector3.right * postWidth;
		Vector3 forwardDir = rotation * Vector3.forward * postWidth;
	
		Vector3 farCorner = upDir + rightDir + forwardDir + position;
		Vector3 nearCorner = position;
	
		//shift pivot to centre-bottom:
		Vector3 pivotOffset = (rightDir + forwardDir) * 0.5f;
		farCorner -= pivotOffset;
		nearCorner -= pivotOffset;
	
		BuildQuad(meshBuilder, nearCorner, rightDir, upDir);
		BuildQuad(meshBuilder, nearCorner, upDir, forwardDir);
	
		BuildQuad(meshBuilder, farCorner, -rightDir, -forwardDir);
		BuildQuad(meshBuilder, farCorner, -upDir, -rightDir);
		BuildQuad(meshBuilder, farCorner, -forwardDir, -upDir);
	}
	
	
	public void BuildCrossPiece(MeshBuilder meshBuilder, Vector3 firstPost, Vector3 secondPost, float crossPieceWidth, float crossPieceHeight )
	{
		
		Vector3 dir = secondPost - firstPost;

		Quaternion rotation = Quaternion.LookRotation(dir);
		
		
		Vector3 upDir = rotation * Vector3.up * crossPieceHeight;
		Vector3 rightDir = rotation * Vector3.right * crossPieceWidth;
		Vector3 forwardDir = rotation* Vector3.forward * ((secondPost-firstPost).magnitude);

		Vector3 farCorner = upDir + rightDir + forwardDir + firstPost;
		Vector3 nearCorner = firstPost;
	
		BuildQuad(meshBuilder, nearCorner, forwardDir, rightDir);
		BuildQuad(meshBuilder, nearCorner, rightDir, upDir);
		BuildQuad(meshBuilder, nearCorner, upDir, forwardDir);
	
		BuildQuad(meshBuilder, farCorner, -rightDir, -forwardDir);
		BuildQuad(meshBuilder, farCorner, -upDir, -rightDir);
		BuildQuad(meshBuilder, farCorner, -forwardDir, -upDir);
	}
	
	public void BuildFence(MeshBuilder meshBuilder,IList<Vector3> points, float postSeperation, float postWidth, float postHeight){
		
		this.BuildPost(meshBuilder,points[0],postHeight,postWidth,Quaternion.identity);
		
		for(var i=0; i< points.Count-1;i++){
			this.BuildSection(meshBuilder, points[i], points[i+1], postSeperation, postWidth, postHeight);
		}
	}

	public void BuildSection(MeshBuilder meshBuilder, Vector3 fromPoint, Vector3 to, float postSeperation, float postWidth, float postHeight){
		
		Vector3 directionUnitVector = (fromPoint - to).normalized;
		float distanceFromLastPoint = System.Math.Abs((fromPoint - to).magnitude);
		int numPosts = (int)System.Math.Ceiling(distanceFromLastPoint/postSeperation);
		
		for(var i=0; i< numPosts;i++){
				
			Vector3 nextPost = fromPoint - (directionUnitVector* postSeperation);
			
			if((nextPost-fromPoint).magnitude > (to-fromPoint).magnitude){
				nextPost = to;
			}

			//lets build a post
			this.BuildPost(meshBuilder,nextPost,postHeight,postWidth,Quaternion.identity);
			
			this.BuildCrossPiece(meshBuilder, fromPoint +  Vector3.up*1, nextPost +  Vector3.up*1, 0.5f, 1 );

			fromPoint = nextPost;
			
		}
		
	}
}
