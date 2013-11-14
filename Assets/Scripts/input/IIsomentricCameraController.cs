using UnityEngine;
using System.Collections;

public interface IIsomentricCameraController {

	Vector2 CalculateTranslation (Transform camera);
	Vector2 CalculateRotation(Transform camera);
	
}
