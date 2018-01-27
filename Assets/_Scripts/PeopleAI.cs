using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PeopleAI : MonoBehaviour {

    public float waitTime;

    public float waitTimeCounter;
    private float switchProbability = .2f;

    private bool personWaiting;
    private bool personMovingForwards;
    private bool personMoving;

    [SerializeField]
    private int wayPointIndex = 0;

    public List<WayPoint> wayPoints;

    [SerializeField]
    Transform destination;

    NavMeshAgent navMeshAgent;

    

	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if(wayPoints.Count >= 2)
        {
            SetDestination();
            ChangePatrolPoint();
        }
        else
        {
            print("Insufficient Amount of Waypoints on " + gameObject.name);
        }

	}

    private void Update()
    {
        if(personMoving && navMeshAgent.remainingDistance <= 1f)
        {
            personWaiting = true;
            waitTimeCounter = waitTime;
        }

        if (personWaiting)
        {
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <= 0)
            {
                personWaiting = false;
                SetDestination();
            }
        }
    }

    void SetDestination()
    {
        if(destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(wayPoints[wayPointIndex].transform.position);
            personMoving = true;
        }
    }

    void ChangePatrolPoint()
    {
        if(Random.Range(0f, 1f) <= switchProbability)
        {
            personMovingForwards = !personMovingForwards;
        }
        if (personMovingForwards)
        {
            wayPointIndex = (wayPointIndex + 1) % wayPoints.Count;
        }
        else
        {
            if(wayPointIndex < 0)
            {
                wayPointIndex = wayPoints.Count - 1;
            }
        }
    }
}
