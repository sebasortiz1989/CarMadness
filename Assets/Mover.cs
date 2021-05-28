using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public Transform[] waypoints;
    NavMeshAgent agent;
    NavMeshPath currentPath = null;
    int currentWP = 0;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (waypoints.Length > 0)
        {
            //agent.SetDestination(waypoints[currentWP].position);
            UpdatePath();

        }
    }

    void Update()
    {
        if (agent.remainingDistance <= 1f)
        {

            currentWP++;
            currentWP %= waypoints.Length;
            UpdatePath();
        }
        // if (currentWP >= waypoints.Length)
        // {
        //     currentWP = 0;
        // }

        //agent.SetDestination(waypoints[currentWP].position);
    }

    private void UpdatePath()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(waypoints[currentWP].position, path);
        currentPath = path;
    }

    public NavMeshPath GetCurrentNavPath()
    {
        return currentPath;
    }
}
