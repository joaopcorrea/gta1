using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    public float maxSpeed;
    public float torque;
    public float brakeTorque;
    public float steer;
    public Transform driverDoor;

    public WheelCollider frontRightWheel;
    public WheelCollider frontLeftWheel;
    public WheelCollider backRightWheel;
    public WheelCollider backLeftWheel;

    private Rigidbody rb;

    private Vector2 directionDrive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // directionDrive = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        
    }

    public void Drive(float direction)
    {
        if (direction < 0 && rb.velocity.z > 0)
        {
            backRightWheel.brakeTorque = direction * brakeTorque * -1;
            backLeftWheel.brakeTorque = direction * brakeTorque * -1;
        }
        else
        {
            backRightWheel.brakeTorque = 0;
            backLeftWheel.brakeTorque = 0;
        }

        backRightWheel.motorTorque = direction * torque;
        backLeftWheel.motorTorque = direction * torque;
    }

    public void Turn(float direction)
    {
        frontRightWheel.steerAngle = direction * steer;
        frontLeftWheel.steerAngle = direction * steer;
    }
}
