using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PeopleAI : MonoBehaviour {

    [SerializeField]
    Transform destination;
    NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();

        SetDestination();
	}
	
	void SetDestination()
    {
        if(destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }
}
