using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform carTransform;
    public Transform cameraPointTransform;
    private Vector3 velocity = Vector3.zero;
    void FixedUpdate()
    {
        transform.LookAt(carTransform);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPointTransform.position, ref velocity, 5f * Time.deltaTime);
    }
}
