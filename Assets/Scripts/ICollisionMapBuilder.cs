using UnityEngine;
using System.Collections;

public interface ICollisionMapBuilder  {

	bool IsCollision(Vector3 point);
	void BuildMap();
	void AddGameObjectToMap(GameObject gameObject);
}
