// Created by the GameDev.tv team. Let us know what cool things you create
// using this! https://GameDev.tv

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
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
        MovementInput();
        TurnVehicle();
        MoveCarBodyWithSphere();
    }

    private void MovementInput()
    {
        moveInput = Input.GetAxisRaw("Vertical");
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
        turnInput = Input.GetAxisRaw("Horizontal");
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
            print("I am grounded, yo");
        }
        else
        {
            print("well, well, it appears I'm not touching what I believe to be the ground, dude");
        }
    }

}
