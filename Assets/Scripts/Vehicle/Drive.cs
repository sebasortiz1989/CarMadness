using System;
using System.Collections;
using UnityEngine;

namespace Vehicles.Vehicle
{
    public class Drive : MonoBehaviour
    {
        [SerializeField] bool manualDrive;

        [Header("Manual Drive Setttings")]
        [SerializeField] float manualMaxTorque = 500;
        [SerializeField] float manualMaxSteerAngle = 30;
        [SerializeField] float manualMaxBrakeTorque = 1000;
        [SerializeField] float maxRotation = 10;

        [Header("Wheel references")]
        [SerializeField] GameObject[] wheelMeshes;
        [SerializeField] WheelCollider[] wheelColliders;

        // Initialize variables
        Quaternion quaternion;
        Vector3 position;

        // Initialize Variables
        private float accelerate;
        private float steer;
        private float brake;
        private bool stabilizingCar;
        public float zRotation;

        // Update is called once per frame
        void Update()
        {
            ManualDrive();
            //PreventCarFromTipingOver();
        }

        private void PreventCarFromTipingOver()
        {
            zRotation = WrapAngle(this.transform.localEulerAngles.z);
            if (Mathf.Abs(zRotation) > maxRotation && !stabilizingCar)
            {
                stabilizingCar = true;
            }

            if (Mathf.Abs(zRotation) > 3)
            {
                if (stabilizingCar)
                {
                    float rotatingAngle = -Mathf.Sign(zRotation);
                    this.transform.Rotate(Vector3.forward, rotatingAngle);
                }
                else
                {
                    stabilizingCar = false;
                }
            }
        }

        private float WrapAngle(float angle)
        {
            angle %= 360;
            if (angle > 180)
                return angle - 360;
            return angle;
        }

        private void ManualDrive()
        {
            if (manualDrive)
            {
                accelerate = Input.GetAxis("Vertical");
                steer = Input.GetAxis("Horizontal");
                brake = Input.GetAxis("Jump");
                GoManual(accelerate, steer, brake);
            }
        }

        private void GoManual(float accel, float steer, float brake)
        {
            accel = Mathf.Clamp(accel, -1, 1);
            steer = Mathf.Clamp(steer, -1, 1);
            brake = Mathf.Clamp(brake, 0, 1);

            float acceleration = Mathf.Clamp(accel, -1, 1) * manualMaxTorque;
            float steering = steer * manualMaxSteerAngle;
            float braking = brake * manualMaxBrakeTorque;

            for (int i = 0; i < 4; i++)
            {
                wheelColliders[i].motorTorque = acceleration;
                wheelColliders[i].brakeTorque = braking;

                if (i > 1)
                    wheelColliders[i].steerAngle = steering;

                wheelColliders[i].GetWorldPose(out position, out quaternion);
                wheelMeshes[i].transform.position = position;
                wheelMeshes[i].transform.rotation = quaternion;
            }
        }

        ////////////////////// This functions will help us when we start implementing AI to the enemy vehicles //////////////////////////
        public void Accelerate(float BLW_accelTorque = 0, float BRW_accelTorque = 0, float FLW_accelTorque = 0, float FRW_accelTorque = 0)
        {
            if (float.IsNaN(BLW_accelTorque) || float.IsNaN(BRW_accelTorque) || float.IsNaN(FLW_accelTorque) || float.IsNaN(FRW_accelTorque))
            {
                BLW_accelTorque = 0;
                BRW_accelTorque = 0;
                FLW_accelTorque = 0;
                FRW_accelTorque = 0;
            }

            wheelColliders[0].motorTorque = BLW_accelTorque;
            wheelColliders[1].motorTorque = BRW_accelTorque;
            wheelColliders[2].motorTorque = FLW_accelTorque;
            wheelColliders[3].motorTorque = FRW_accelTorque;

            // Syncronizes the position of wheel collider to the wheel mesh.
            for (int i = 0; i < 4; i++)
            {
                wheelColliders[i].GetWorldPose(out position, out quaternion);
                wheelMeshes[i].transform.position = position;
                wheelMeshes[i].transform.rotation = quaternion;
            }
        }

        public void Brake(float BLW_brakeTorque = 0, float BRW_brakeTorque = 0, float FLW_brakeTorque = 0, float FRW_brakeTorque = 0)
        {
            if (float.IsNaN(BLW_brakeTorque) || float.IsNaN(BRW_brakeTorque) || float.IsNaN(FLW_brakeTorque) || float.IsNaN(FRW_brakeTorque))
            {
                BLW_brakeTorque = 0;
                BRW_brakeTorque = 0;
                FLW_brakeTorque = 0;
                FRW_brakeTorque = 0;
            }

            wheelColliders[0].brakeTorque = BLW_brakeTorque;
            wheelColliders[1].brakeTorque = BRW_brakeTorque;
            wheelColliders[2].brakeTorque = FLW_brakeTorque;
            wheelColliders[3].brakeTorque = FRW_brakeTorque;

            // Syncronizes the position of wheel collider to the wheel mesh.
            for (int i = 0; i < 4; i++)
            {
                wheelColliders[i].GetWorldPose(out position, out quaternion);
                wheelMeshes[i].transform.position = position;
                wheelMeshes[i].transform.rotation = quaternion;
            }
        }

        public void Steer(float steerAngleMagnitude = 0, float maxSteerAngle = 0)
        {
            wheelColliders[2].steerAngle = steerAngleMagnitude * maxSteerAngle;
            wheelColliders[3].steerAngle = steerAngleMagnitude * maxSteerAngle;

            // Syncronizes the position of wheel collider to the wheel mesh.
            for (int i = 0; i < 4; i++)
            {
                wheelColliders[i].GetWorldPose(out position, out quaternion);
                wheelMeshes[i].transform.position = position;
                wheelMeshes[i].transform.rotation = quaternion;
            }
        }
    }
}