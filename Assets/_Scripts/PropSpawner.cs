using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour {
    public int minObjectsToSpawn;
    public int maxObjectsToSpawn;

    private int objectsToSpawn;

    public float minObjectSpawnTime;
    public float maxObjectSpawnTime;

    private float objectSpawnTimer;

    public GameObject newGameObject;

    public LayerMask collisionsMask;
    public RaycastHit hit;

    private Vector3 point;

    public float spawnRadius;
    // Use this for initialization
    void Start () {
        objectSpawnTimer = Random.Range(minObjectSpawnTime, maxObjectSpawnTime);
        objectsToSpawn = Random.Range(minObjectsToSpawn, maxObjectsToSpawn);
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(transform.position, Vector3.down * 5f, out hit, collisionsMask);
        Debug.DrawRay(transform.position, Vector2.down * 5f, Color.red);

        if(objectSpawnTimer < 0 &&  objectsToSpawn > 0)
        {
            point = new Vector3(Random.Range(hit.point.x - spawnRadius, hit.point.x + spawnRadius - 1), hit.point.y + 1f, Random.Range(hit.point.z - spawnRadius, hit.point.z + spawnRadius - 1));

            while (Physics.CheckSphere(point, .5f, collisionsMask) == true)
            {
                point = new Vector3(Random.Range(hit.point.x - spawnRadius, hit.point.x + spawnRadius - 1), hit.point.y + 1f, Random.Range(hit.point.z - spawnRadius, hit.point.z + spawnRadius - 1));
            }
  
            Instantiate(newGameObject, point, Quaternion.identity);

            objectsToSpawn -= 1;
            objectSpawnTimer = minObjectSpawnTime;
        }

        objectSpawnTimer -= Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Physics.Raycast(transform.position, Vector3.down * 5f, out hit, collisionsMask);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .5f);
        Gizmos.DrawWireSphere(hit.point, spawnRadius);
    }
}
