using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 50f;
    public float turnSpeed = 150f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
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
  }
}
