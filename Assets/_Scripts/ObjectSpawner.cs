using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public LayerMask collisionsMask;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        Debug.DrawRay(transform.position, Vector2.down * 5f, Color.red);
        

        if (Physics.Raycast(transform.position, Vector3.down, 5f, collisionsMask))
        {
            //To Do, shoot a raycast down to the ground, if there is a collision, check the surroundings in a circle area
            //Spawn an object at the area, if the area is within range of an object, then respawn the object in a different area.
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
