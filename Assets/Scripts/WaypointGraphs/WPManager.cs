using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum Direction { UNI, BI };
    public GameObject[] waypoints;
    public GameObject node1;
    public GameObject node2;
    public Direction direction;
}

public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    void Start()
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject waypoint in waypoints)
            {
                graph.AddNode(waypoint);
            }
            foreach (Link link in links)
            {
                graph.AddEdge(link.node1, link.node2);
                if (link.direction == Link.Direction.BI)
                {
                    graph.AddEdge(link.node2, link.node1);
                }
            }
        }

    }

    void Update()
    {
        graph.debugDraw();
    }
}
