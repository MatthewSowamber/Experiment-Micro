using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    NavMeshAgent agent;
    float reachTargetRange = 2f;
    [SerializeField] int currentWayPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < reachTargetRange)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        //At the next way point we increment the value so that they move to the next waypoint
        currentWayPoint++;
        if (currentWayPoint >= waypoints.Length)
            currentWayPoint = 0;
        agent.SetDestination(waypoints[currentWayPoint].transform.position);
    }
}