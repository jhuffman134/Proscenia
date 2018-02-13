using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHitBoxManager : MonoBehaviour {

    // Set these in the editor, works for any number of colliders
    public PolygonCollider2D noAttack;
    public PolygonCollider2D Attack;


    // Used for organization
    private PolygonCollider2D[] colliders;

    // Collider on this game object
    private PolygonCollider2D localCollider;

    // We say box, but we're still using polygons.
    //public enum hitBoxes
    //{
    //    frame2Box,
    //    frame3Box,
    //    clear // special case to remove all boxes
    //}

    void Start()
    {
        // Set up an array so our script can more easily set up the hit boxes
        colliders = new PolygonCollider2D[] {noAttack, Attack};

        // Create a polygon collider
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
        localCollider.pathCount = 0; // Clear auto-generated polygons
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // add slight knockback?

        Debug.Log("Collider hit something!");
    }

    public void setKnightHitBox(int val)
    {
        if (val >= 0)
        {
            localCollider.SetPath(0, colliders[val].GetPath(0));
        }
    }
}
