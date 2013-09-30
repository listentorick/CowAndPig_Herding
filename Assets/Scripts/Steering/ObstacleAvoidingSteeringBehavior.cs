using UnityEngine;
using System.Collections;
using UnitySteer;
using UnitySteer.Helpers;

public class ObstacleAvoidingSteeringBehavior : Steering {

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	[SerializeField]
	CollisionMapBuilder _collisionMapBuilder;
	
	public CollisionMapBuilder CollisionMapBuilder {
		get {
			return this._collisionMapBuilder;
		}
		set {
			_collisionMapBuilder = value;
		}
	}
	
	[SerializeField]
	float _avoidanceForceFactor = 0.75f;
	
	public float AvoidanceForceFactor {
		get {
			return this._avoidanceForceFactor;
		}
		set {
			_avoidanceForceFactor = value;
		}
	}
	
	[SerializeField]
	float _minTimeToCollision = 2;
	
	
	public float MinTimeToCollision {
		get {
			return this._minTimeToCollision;
		}
		set {
			_minTimeToCollision = value;
		}
	}
	
	protected override Vector3 CalculateForce()
	{
		 /*
		 * While we could just calculate line as (Velocity * predictionTime) 
		 * and save ourselves the substraction, this allows other vehicles to
		 * override PredictFuturePosition for their own ends.
		 */
		Vector3 futurePosition = Vehicle.PredictFuturePosition(_minTimeToCollision);
		//Debug.DrawLine(Vehicle.Position, futurePosition, Color.white);
		Vector3 movement = futurePosition - Vehicle.Position;
		float mag = movement.magnitude;
		float sampleSize = 100;
		//sample n points along
		movement.Normalize();
		
		for(var i=0; i<sampleSize;i++){
		
			Vector3 targetPosition =  Vehicle.Position + (movement * ((mag/sampleSize)*i));
			
			if(CollisionMapBuilder.IsCollision(targetPosition)){
				return CalculateAvoidanceForce(targetPosition);
				break;
			}
		}
		
		return Vector3.zero;

	}	
	
	private Vector3 CalculateAvoidanceForce(Vector3 futurePosition){ 
		
			//Debug.Log("COLLISION" + futurePosition);
			
			Vector3 offset =  Vehicle.Position - futurePosition;
		
			var movement = futurePosition - Vehicle.Position;
			Vector3 moveDirection = movement.normalized;
			Vector3 avoidance = Vector3.zero;
		
		avoidance = Quaternion.AngleAxis(-10, Vector3.up) * movement;
		
		
	//	avoidance =	 OpenSteerUtility.perpendicularComponent(movement, moveDirection);
	
	//		avoidance = avoidance.normalized * Vehicle.MaxForce * _avoidanceForceFactor;
		
			//avoidance += moveDirection * Vehicle.MaxForce * _avoidanceForceFactor;
			
			Debug.DrawLine(Vehicle.Position, Vehicle.Position + avoidance, Color.yellow);

			return avoidance;
		
	}
}
