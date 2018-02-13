using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFollow : MonoBehaviour {

    public float speed = 0.1f;
    private float fastSpeed;
    public float leftRightVariance = 1.5f;
    public float upDownVariance = 1.5f;
    public float leftBound;
    public float rightBound;
    public float upperBound;
    public float lowerBound;
    public GameObject target
        ;
	// Update is called once per frame
	void FixedUpdate () {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > leftRightVariance)
        {
            if (target.transform.position.x > transform.position.x)
            {
                if (transform.position.x < rightBound)
                {
                    transform.position += Vector3.right * speed;
                }
            }
            else
            {
                if (transform.position.x > leftBound)
                {
                    transform.position += Vector3.left * speed;
                }
            }
        }

        if (Mathf.Abs(transform.position.y - target.transform.position.y) > upDownVariance)
        {
            if (target.transform.position.y > transform.position.y)
            {
                if (transform.position.y < upperBound)
                {
                    transform.position += Vector3.up * speed;
                }
            }
            else
            {
                if (transform.position.y > lowerBound)
                { 
                    transform.position += Vector3.down * speed;
                }
            }
        }

    }
}
