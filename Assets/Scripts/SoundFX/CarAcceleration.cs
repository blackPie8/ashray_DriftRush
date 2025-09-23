using UnityEngine;

public class CarAcceleration : MonoBehaviour
{
    [SerializeField] private AudioSource carAcceleration;
    [SerializeField] private float maxPitch = 2.0f;
    [SerializeField] private float minPitch = 0f;
    [SerializeField] private float maxSpeed = 40f;
    private float minSpeed = 1f;

    void Start()
    {
        if (carAcceleration != null)
        {
            carAcceleration.loop = true;
            carAcceleration.playOnAwake = false;
        }
    }

    void FixedUpdate()
    {
        float speed = PhysicsCarMovement.Instance.CarSpeed();
        if (speed > minSpeed)
        {
            if (!carAcceleration.isPlaying)
            {
                carAcceleration.Play();
            }

                float pitch = Mathf.Lerp(minPitch, maxPitch, speed / maxSpeed);
                carAcceleration.pitch = pitch;
                carAcceleration.volume = 1;
        }
        else
        {
            if (carAcceleration.isPlaying)
            {
                carAcceleration.volume = Mathf.Lerp(carAcceleration.volume, 0f, 5f * Time.fixedDeltaTime);

                if (carAcceleration.volume < 0.01f)
                {
                    carAcceleration.Stop();
                }
            }
        }
    }
}
