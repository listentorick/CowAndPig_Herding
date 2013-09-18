using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    	float translationX  = Input.GetAxis("Horizontal");
    	float translationY = Input.GetAxis("Vertical");

    	this.transform.Translate(translationX + translationY, 0, translationY - translationX); 

	}
}
