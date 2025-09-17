using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float acceleration = 20f;
    public float maxSpeed = 50f;
    private float turnSpeed = 50f;
    public ParticleSystem leftTireSmoke;
    public ParticleSystem rightTireSmoke;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        var emissionLeft = leftTireSmoke.emission;
        var emissionRight = rightTireSmoke.emission;

        float baseRate = 20f;

        float speedFactor = rb.linearVelocity.magnitude / baseRate;

        emissionLeft.rateOverTime = baseRate * speedFactor;
        emissionRight.rateOverTime = baseRate * speedFactor;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 force = transform.forward * vertical * acceleration;

        rb.AddForce(force, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        if (rb.linearVelocity.magnitude > 0.1f)
        {
            float turn = horizontal * turnSpeed * Time.fixedDeltaTime * Mathf.Sign(vertical);
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }


        if (Math.Abs(horizontal) > 0.5f)
        {
            leftTireSmoke.Play();
            rightTireSmoke.Play();
        }
        else
        {
            leftTireSmoke.Stop();
            rightTireSmoke.Stop();
        }
    }
}
