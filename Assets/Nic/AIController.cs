using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform goal;
    [SerializeField] float waypointAccuracy = 1f;
    [SerializeField] UnityStandardAssets.Utility.WaypointCircuit circuit;
    [SerializeField] float turnAngleThreshold = 40f;
    Vector3 goalVector;
    int currentWaypoint = 0;

    [SerializeField] Rigidbody sphereRigidbody;
    [SerializeField] float forwardSpeed;
    [SerializeField] float reverseSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float distanceCheck = .2f;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float gravity = 50f;

    float moveInput;
    float turnInput;
    bool isGrounded;

    void Start()
    {
        // this simply is making sure we don't have issues with the car body following the sphere
        sphereRigidbody.transform.parent = null;
    }

    void FixedUpdate()
    {
        // make sure any objects you want to drive on are tagged as ground layer
        CheckIfGrounded();
        
        if (isGrounded)
        {
            // make car go
            sphereRigidbody.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            // make the car respond to gravity when it is not grounded
            sphereRigidbody.AddForce(transform.up * -gravity);
        }
    }

    void Update()
    {
        SetWaypoint();
        MovementInput();
        TurnVehicle();
        MoveCarBodyWithSphere();
    }

    private void SetWaypoint()
    {
        if (circuit.Waypoints.Length == 0) return;
        // Vector3 lookAtGoal = new Vector3 (circuit.Waypoints[currentWaypoint].transform.position.x,
        //                                     transform.position.y, 
        //                                     circuit.Waypoints[currentWaypoint].transform.position.z);
        // Vector3 direction = lookAtGoal - transform.position;
        // transform.rotation = Quaternion.Slerp(transform.rotation, 
        //                                     Quaternion.LookRotation(direction), 
        //                                     Time.deltaTime * rotSpeed);
        goal = circuit.Waypoints[currentWaypoint].transform;
        if (Vector3.Distance(goal.transform.position, transform.position) < waypointAccuracy)
        {
            currentWaypoint++;
            if (currentWaypoint >= circuit.Waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        
    }

    
    private void MovementInput()
    {
        //moveInput = Input.GetAxisRaw("Vertical");
        goalVector = goal.position - transform.position;
        if (goalVector.magnitude > 1) // todo fix magic number accuracy
        {
            moveInput = 1;
        }
        else
        {
            moveInput = -1;
        }
        
        if (moveInput > 0)
        {
            moveInput *= forwardSpeed;
        }
        else
        {
            moveInput *= reverseSpeed;
        }
    }

    void TurnVehicle()
    {
        //turnInput = Input.GetAxisRaw("Horizontal");

        if (Vector3.Angle(transform.forward, goalVector) > turnAngleThreshold) 
        {
            if (transform.InverseTransformVector(goalVector).x <= 0)
            {
                turnInput = -1;
            }
            else
            {
                turnInput = 1;
            }
        }
        else
        {
            turnInput = 0;
        }


        RaycastHit hitInfoRight;
        if (Physics.Raycast(transform.position, transform.right + transform.forward, out hitInfoRight, 4f))
        {
            if (hitInfoRight.transform != this.transform && (hitInfoRight.transform.gameObject.tag == "Obstacle" || 
                                                                hitInfoRight.transform.gameObject.tag == "Car"))
            {
                print("found collider on the right side");
                turnInput = -1;
            }
        }
        

        RaycastHit hitInfoLeft;
        if (Physics.Raycast(transform.position, -transform.right + transform.forward, out hitInfoLeft, 4f))
        {
            if (hitInfoLeft.transform != this.transform && (hitInfoLeft.transform.gameObject.tag == "Obstacle" || 
                                                                hitInfoLeft.transform.gameObject.tag == "Car"))
            {
                turnInput = 1;
            }
        }

       
        //turnInput = goalVector.normalized.x;
        float newRotation = turnInput * turnSpeed * Time.deltaTime;
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    void MoveCarBodyWithSphere()
    {
        // With your car game object, be sure that the car body and sphere start in exactly the same position
        // or else things go wrong pretty quickly. The next line is making the car body follow the spehere.
        transform.position = sphereRigidbody.transform.position;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(transform.position, distanceCheck, groundLayers, QueryTriggerInteraction.Ignore);
        if (isGrounded)
        {
            //print("I am grounded, yo");
        }
        else
        {
            //print("well, well, it appears I'm not touching what I believe to be the ground, dude");
        }
    }
}
