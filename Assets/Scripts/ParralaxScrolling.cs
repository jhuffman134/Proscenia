using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxScrolling : MonoBehaviour {

    public Transform camera;
    public float parralax;
    private float initialCameraPosition;
	// Use this for initialization
	void Start () {
        initialCameraPosition = camera.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3((camera.position.x - initialCameraPosition) * parralax, 0);
	}
}
