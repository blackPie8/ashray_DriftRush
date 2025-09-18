using UnityEngine;

public class FollowCar : MonoBehaviour
{
    private Transform playerCarTransform;
    private Transform cameraPointTransform;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraPointTransform = playerCarTransform.Find("CameraPoint").GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        transform.LookAt(playerCarTransform);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPointTransform.position, ref velocity, 5f * Time.deltaTime);
    }
}
