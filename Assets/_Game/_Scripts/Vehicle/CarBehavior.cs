using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    // public float maxSpeed;
    // public float torque;
    // public float brakeTorque;
    // public float steer;
    // public Transform driverDoor;

    private float baseForce = 100f;

    private float currentBrakeForce;
    private float currentSteerAngle;


    [SerializeField] private float accelerationForce;
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private Rigidbody rb;

    private Vector2 directionDrive;



    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";


    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {
        rb.AddForce(transform.forward * verticalInput * accelerationForce);
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce * baseForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce * baseForce;
        currentBrakeForce = isBraking ? brakeForce * baseForce : 0f;
        
        ApplyBraking();
    }
    private void ApplyBraking()
    {
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;

        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }


    // public void Drive(float direction)
    // {
    //     if (direction < 0 && rb.velocity.z > 0)
    //     {
    //         rearRightWheelCollider.brakeTorque = direction * brakeTorque * -1;
    //         rearLeftWheelCollider.brakeTorque = direction * brakeTorque * -1;
    //     }
    //     else
    //     {
    //         rearRightWheelCollider.brakeTorque = 0;
    //         rearLeftWheelCollider.brakeTorque = 0;
    //     }

    //     rearRightWheelCollider.motorTorque = direction * torque;
    //     rearLeftWheelCollider.motorTorque = direction * torque;
    // }

    // public void Turn(float direction)
    // {
    //     frontRightWheelCollider.steerAngle = direction * steer;
    //     frontLeftWheelCollider.steerAngle = direction * steer;
    // }
}
