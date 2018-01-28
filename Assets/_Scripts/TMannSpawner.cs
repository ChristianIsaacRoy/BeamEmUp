using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMannSpawner : MonoBehaviour {

    public GameObject TMann;
    public GameEvent OnTMannSpawn;

    private bool alreadySpawned = false;
    public float TMannSpawnTime;
    public float spawnRadius;
    public LayerMask LayerMask;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

        if (TMannSpawnTime <= 0 && alreadySpawned == false)
        {
            alreadySpawned = true;
            SpawnTMann();
        }

        TMannSpawnTime -= Time.deltaTime;
    }

    private void SpawnTMann()
    {
        OnTMannSpawn.Raise();

        RaycastHit hit = new RaycastHit();
        Physics.Raycast(transform.position, Vector3.down * 5f, out hit, LayerMask);

        Vector3 point = new Vector3(Random.Range(hit.point.x - spawnRadius, hit.point.x + spawnRadius - 1), hit.point.y + 2.75f, Random.Range(hit.point.z - spawnRadius, hit.point.z + spawnRadius - 1));

        while (Physics.CheckSphere(point, .5f, LayerMask) == true)
        {
            point = new Vector3(Random.Range(hit.point.x - spawnRadius, hit.point.x + spawnRadius - 1), hit.point.y + 2.75f, Random.Range(hit.point.z - spawnRadius, hit.point.z + spawnRadius - 1));
        }

        Instantiate(TMann, point, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Physics.Raycast(transform.position, Vector3.down * 5f, out hit, LayerMask);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .5f);
        Gizmos.DrawWireSphere(hit.point, spawnRadius);
    }



}
