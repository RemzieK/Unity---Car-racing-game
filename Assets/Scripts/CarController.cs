using System;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  public enum CarType
    {
        FrontWheelDrive,
        RearWheelDrive,
        FourWheelDrive
    }
    public CarType carType = CarType.FrontWheelDrive;

    public enum ControlMode
    {
        Keyboard,
        Button
    }

    public ControlMode control;

    [Header("Wheel GameObject Meshes")]
    public GameObject FrontWheelLeft;
    public GameObject FrontWheelRight;
    public GameObject BackWheelLeft;
    public GameObject BackWheelRight;

    [Header("WheelCollider")]
    public WheelCollider FrontWheelLeftCollider;
    public WheelCollider FrontWheelRightCollider;
    public WheelCollider BackWheelLeftCollider;
    public WheelCollider BackWheelRightCollider;

    [Header("Movement, Steering and Braking")]
    private float currentSpeed;
    public float maximumMotorTorque;
    public float maximumSteeringAngle=20f;
    public float maximumSpeed;
    public float brakePower;
    public Transform COM;
    private float carSpeed;
    private float carSpeedConverted;
    private float motorTorque;
    private float tireAngle;
    private float vertical = 0f;
    private float horizontal = 0f;
    private bool handBrake = false;
    private Rigidbody carRigidBody;

    void Start()
    {
        carRigidBody = GetComponent<Rigidbody>();

        if(carRigidBody != null)
        {
            carRigidBody.centerOfMass = COM.localPosition;
        }

    }

    void Update()
    {
        GetInputs();
        CalculateCarMovement();
        CalculateSteering();

        ApplyTranformToWheels();
    }

    void GetInputs()
    {
        if(control == ControlMode.Keyboard)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
    }

    void CalculateCarMovement()
    {
        carSpeed = carRigidBody.velocity.magnitude;
        carSpeedConverted = Mathf.Round(carSpeed * 3.6f);

        //braking
        if (Input.GetKey(KeyCode.Space))
        {
            handBrake = true;
        }
        else
        {
            handBrake = false;
        }
        if (handBrake)
        {
            motorTorque = 0;
            ApplyBrake();
        }
        else
        {
            ReleaseBrake();
            if (carSpeedConverted < maximumSpeed)
            {
                motorTorque = maximumMotorTorque * vertical;
            }
            else
            {
                motorTorque = 0;
            }
        }

        ApplyMotorTorque();
    }

    void CalculateSteering()
    {
        float speedFactor = Mathf.Clamp01(carSpeed / maximumSpeed);
        float reducedSteeringAngle = Mathf.Lerp(maximumSteeringAngle, maximumSteeringAngle / 2, speedFactor);
        tireAngle = maximumSteeringAngle * horizontal;
        FrontWheelLeftCollider.steerAngle = tireAngle;
        FrontWheelRightCollider.steerAngle = tireAngle;
    }


    void ApplyMotorTorque()
    {
        if(carType == CarType.FrontWheelDrive)
        {
            FrontWheelLeftCollider.motorTorque = motorTorque;
            FrontWheelRightCollider.motorTorque = motorTorque;
        }
        else if (carType == CarType.RearWheelDrive)
        {
            BackWheelLeftCollider.motorTorque = motorTorque;
            BackWheelRightCollider.motorTorque = motorTorque;
        }
        else if (carType == CarType.FourWheelDrive)
        {
            FrontWheelLeftCollider.motorTorque = motorTorque;
            FrontWheelRightCollider.motorTorque = motorTorque;
            BackWheelLeftCollider.motorTorque = motorTorque;
            BackWheelRightCollider.motorTorque = motorTorque;
        }
    }

    void ApplyBrake()
    {
        FrontWheelLeftCollider.brakeTorque = brakePower;
        FrontWheelRightCollider.brakeTorque = brakePower;
        BackWheelLeftCollider.brakeTorque = brakePower;
        BackWheelRightCollider.brakeTorque = brakePower;
    }

    void ReleaseBrake()
    {
        FrontWheelLeftCollider.brakeTorque = 0;
        FrontWheelRightCollider.brakeTorque = 0;
        BackWheelLeftCollider.brakeTorque = 0;
        BackWheelRightCollider.brakeTorque = 0;
    }

    public void ApplyTranformToWheels()
    {
        Vector3 position;
        Quaternion rotation;

        FrontWheelLeftCollider.GetWorldPose(out position, out rotation);
        FrontWheelLeft.transform.position = position;
        FrontWheelLeft.transform.rotation = rotation;

        FrontWheelRightCollider.GetWorldPose(out position, out rotation);
        FrontWheelRight.transform.position = position;
        FrontWheelRight.transform.rotation = rotation;

        BackWheelLeftCollider.GetWorldPose(out position, out rotation);
        BackWheelLeft.transform.position = position;
        BackWheelLeft.transform.rotation = rotation;

        BackWheelRightCollider.GetWorldPose(out position, out rotation);
        BackWheelRight.transform.position = position;
        BackWheelRight.transform.rotation = rotation;
    }
}
