using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {
	
    private float xPos;
    private float yPos;
    private float height;
    public float jumpHeight;
    public float timeMultiplier1;
    public float timeMultiplier2;
    void Start()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
    }
	// Update is called once per frame
	void Update () {
        /*
        height = Mathf.Cos(Time.time * timeMultiplier1) * jumpHeight;
        if (height > 0)
        {
            transform.position = new Vector3(xPos, yPos + height, 0);
        }
        */
        height = (Mathf.Cos(Time.time * timeMultiplier1) + Mathf.Cos(Time.time * timeMultiplier2)) * jumpHeight;
        if(height > 0)
        {
            transform.position = new Vector3(xPos, yPos + height, 0);
        }

	}
}
