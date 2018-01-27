using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour { 
    public List<GameObject> objectList;
    private GameObject temp;

    public LayerMask collisionsMask;
    public RaycastHit hit;

    private Vector3 point;

    public float spawnRadius;
    // Use this for initialization
    void Start () {
        RaycastHit hit = new RaycastHit();
        //Shoots a ray downwards
        Physics.Raycast(transform.position, Vector3.down * 5f, out hit, collisionsMask);

        //If the point we are at is inside a ground layer, then we change the spawn location.
        point = new Vector3(Random.Range(hit.point.x - spawnRadius, hit.point.x + spawnRadius), hit.point.y + 1, Random.Range(hit.point.z - spawnRadius, hit.point.z + spawnRadius));
        while (Physics.CheckSphere(point, .5f, collisionsMask) == true) {
            point = new Vector3(Random.Range(hit.point.x - spawnRadius, hit.point.x + spawnRadius), hit.point.y + 1, Random.Range(hit.point.z - spawnRadius, hit.point.z + spawnRadius));
        }

        //Choose a random object from our object list and instantiate it.
        int rand = Random.Range(0, objectList.Count);
        temp = objectList[rand];
        Instantiate(temp, point, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, Vector2.down * 5f, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hit.point, spawnRadius);
    }
}
