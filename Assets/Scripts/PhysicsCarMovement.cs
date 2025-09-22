using System;
using UnityEngine;

public class PhysicsCarMovement : MonoBehaviour
{
    public static PhysicsCarMovement Instance { get; private set; }
    public delegate void CarStoppedHandler();
    public event CarStoppedHandler OnCarStopped;

    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform carCentreOfMassTransform;
    [SerializeField] private TrailRenderer rearLeftTrailRenderer;
    [SerializeField] private TrailRenderer rearRightTrailRenderer;
    [SerializeField] private ParticleSystem rearLeftParticleSystem;
    [SerializeField] private ParticleSystem rearRightParticleSystem;
    [SerializeField] private float motorForce = 300f;
    [SerializeField] private float steerAngle = 30f;
    [SerializeField] private float brakeForce = 1000f;
    [SerializeField] private float endRaceBrakeForce = 7f;
    private bool isBraking = false;
    private Rigidbody rb;
    float verticalInput;
    float horizontalInput;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = carCentreOfMassTransform.localPosition;
    }

    void FixedUpdate()
    {
        GetInput();
        MotorForce();
        UpdateWheels();
        Steering();
        ApplyBrakes();
        CheckDrift();

        if (isBraking)
        {
            SlowDownCar();
        }
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
            rb.linearDamping = 1f;
        }
        else
        {
            frontRightWheelCollider.brakeTorque = 0f;
            frontLeftWheelCollider.brakeTorque = 0f;
            rearLeftWheelCollider.brakeTorque = 0f;
            rearRightWheelCollider.brakeTorque = 0f;
            rb.linearDamping = 0f;
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
    public float CarSpeed()
    {
        if (rb == null) return 0f;

        float speed = rb.linearVelocity.magnitude * 3.6f;
        return speed;
    }

    void CheckDrift()
    {
        WheelHit leftHit;
        WheelHit rightHit;

        // if (rearLeftTrailRenderer == null || rearRightTrailRenderer == null) return;

        if (rearLeftWheelCollider.GetGroundHit(out leftHit) && rearRightWheelCollider.GetGroundHit(out rightHit))
        {
            bool leftDrifting = Math.Abs(leftHit.sidewaysSlip) > 0.2f;
            bool rightDrifting = Math.Abs(rightHit.sidewaysSlip) > 0.2f;

            rearLeftTrailRenderer.emitting = leftDrifting;
            rearRightTrailRenderer.emitting = rightDrifting;

            if (leftDrifting && rightDrifting)
            {
                if (!rearLeftParticleSystem.isPlaying)
                    rearLeftParticleSystem.Play();

                if (!rearRightParticleSystem.isPlaying)
                {
                    rearRightParticleSystem.Play();
                }
            }
            else
            {
                if (rearLeftParticleSystem.isPlaying)
                {
                    rearLeftParticleSystem.Stop();
                }
                if (rearRightParticleSystem.isPlaying)
                {
                    rearRightParticleSystem.Stop();
                }
            }
        }
        else
        {
            rearLeftTrailRenderer.emitting = false;
            rearRightTrailRenderer.emitting = false;
            rearLeftParticleSystem.Stop();
            rearRightParticleSystem.Stop();
        }
    }

    void SlowDownCar()
    {
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, endRaceBrakeForce * Time.fixedDeltaTime);

        if (rb.linearVelocity.magnitude < 0.1f)
        {
            rb.linearVelocity = Vector3.zero;
            isBraking = false;

            OnCarStopped?.Invoke();
        }
    }

    public void StartBraking()
    {
        isBraking = true;
    }
}