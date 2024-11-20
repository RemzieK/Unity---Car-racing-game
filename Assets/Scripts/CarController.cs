using System;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    public float maxAcceleration = 80.0f;
    public float brakeAcceleration = 100.0f;

    public float turnSensitivity = 1.0f;
    public float maxStearingAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass - new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        GetInputs();
        AnimatedWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 1000 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        // Calculate a speed-sensitive steering angle to avoid tipping
        float speedFactor = Mathf.Clamp01(carRb.velocity.magnitude / 50.0f);  // Adjust 50.0f as needed
        float adjustedSteeringAngle = maxStearingAngle * (1 - speedFactor);

        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var targetSteerAngle = steerInput * turnSensitivity * adjustedSteeringAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, targetSteerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 2000 * brakeAcceleration;
            }
        }
        else
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    void AnimatedWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}
