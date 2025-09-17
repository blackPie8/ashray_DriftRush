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


    void FixedUpdate()
    {
        MotorForce();
        UpdateWheels();
    }
    void MotorForce()
    {
        frontRightWheelCollider.motorTorque = 20f;
        frontLeftWheelCollider.motorTorque = 20f;
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
