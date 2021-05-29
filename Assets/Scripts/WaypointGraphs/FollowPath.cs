using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car.Control;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float rotSpeed = 2.0f;
    float accuracy = 8.0f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph graph;
    AIController aiController;

    private void Start() 
    {
        aiController = GetComponent<AIController>();
        wps = wpManager.GetComponent<WPManager>().waypoints;
        graph = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[1];
    }

    void LateUpdate()
    {   
        if (graph.getPathLength() == 0 || currentWP == graph.getPathLength())
        {
            FindNewDestination();
            return;
        }

        // The node we are closest to at the moment
        currentNode = graph.getPathPoint(currentWP);

        if (Vector3.Distance(graph.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;   
        }

        if (currentWP < graph.getPathLength())
        {        
            aiController.SetGoal(graph.getPathPoint(currentWP).transform.position);
        }

    }

    private void FindNewDestination()
    {
        int numDestinations = 3;
        int index = Random.Range(0, numDestinations);
        print ("index: " + index);
        switch (index)
        {
            case 0:
                GoToCenterPlatform();
                break;
            case 1:
                GoToAdjacentPlatform();
                break;
            case 2:
                GoToFarPlatform();
                break;
            default:
                GoToCenterPlatform();
                break;
        }
        
    }

    public void GoToCenterPlatform()
    {
        graph.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    public void GoToFarPlatform()
    {
        graph.AStar(currentNode, wps[23]);
        currentWP = 0;
    }

    public void GoToAdjacentPlatform()
    {
        graph.AStar(currentNode, wps[37]);
        currentWP = 0;
    }
}
