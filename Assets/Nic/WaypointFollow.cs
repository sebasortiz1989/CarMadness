using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    //public GameObject[] waypoints;
    //public UnityStandardAssets.Utility.WaypointCircuit circuit;
    [SerializeField] Rigidbody sphereRigidBody;
    public Transform goal;
    int currentWaypoint = 0;

    float speed = 10.0f;
    float accuracy = 2f;
    float rotSpeed = 2f;
    Rigidbody rb;
    Vector3 movementVector = Vector3.zero;

    void Awake()
    {
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        
    }

    void Update()
    {
        MoveCarWithSphere();
    }

    void LateUpdate()
    {
        //if (circuit.Waypoints.Length == 0) return;

        Vector3 lookAtGoal = new Vector3 (goal.transform.position.x,
                                            transform.position.y, 
                                            goal.transform.position.z);
        Vector3 direction = lookAtGoal - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, 
                                            Quaternion.LookRotation(direction), 
                                            Time.deltaTime * rotSpeed);
        // if (Vector3.Distance(goal.transform.position, transform.position) < accuracy)
        // {
        //     currentWaypoint++;
        //     if (currentWaypoint >= circuit.Waypoints.Length)
        //     {
        //         currentWaypoint = 0;
        //     }
        // }
        movementVector = new Vector3 (0, 0, speed * Time.deltaTime);
        
        //transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void MoveCarWithSphere()
    {
        transform.position = sphereRigidBody.transform.position;
    }

    private void FixedUpdate() 
    {
        sphereRigidBody.AddForce(movementVector);
    }
}
