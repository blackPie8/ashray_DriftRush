using UnityEngine;

public class PhysicsCarMovement : MonoBehaviour
{
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider rearLeftWheelCollider;

    public Transform frontRightWheelTransform;
    public Transform rearRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform frontLeftWheelTransform;

    public Transform carCentreOfMassTransform;
    public Rigidbody rb;
    float verticalInput;
    float horizontalInput;
    public float motorForce = 300f;
    public float steerAngle = 30f;
    public float brakeForce = 1000f;

    void Start()
    {
        rb.centerOfMass = carCentreOfMassTransform.localPosition;
    }

    void FixedUpdate()
    {
        MotorForce();
        UpdateWheels();
        GetInput();
        Steering();
        ApplyBrakes();
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontRightWheelCollider.brakeTorque = brakeForce;
            frontLeftWheelCollider.brakeTorque = brakeForce;
            rearLeftWheelCollider.brakeTorque = brakeForce;
            rearRightWheelCollider.brakeTorque = brakeForce;
        }
        else
        {
            frontRightWheelCollider.brakeTorque = 0f;
            frontLeftWheelCollider.brakeTorque = 0f;
            rearLeftWheelCollider.brakeTorque = 0f;
            rearRightWheelCollider.brakeTorque = 0f;
        }
    }
    void MotorForce()
    {
        frontRightWheelCollider.motorTorque = motorForce * verticalInput;
        frontLeftWheelCollider.motorTorque = motorForce * verticalInput;
    }

    void Steering()
    {
        frontRightWheelCollider.steerAngle = steerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle * horizontalInput;
    }

    void UpdateWheels()
    {
        RotateWheel(frontRightWheelCollider, frontRightWheelTransform);
        RotateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        RotateWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        RotateWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}
