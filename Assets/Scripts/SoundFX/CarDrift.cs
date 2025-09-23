using UnityEngine;

public class CarDrift : MonoBehaviour
{
    [SerializeField] private AudioSource carDrift;

    void FixedUpdate()
    {
        bool isDrifting = PhysicsCarMovement.Instance.isDrifting();
        if (isDrifting && !carDrift.isPlaying)
        {
            carDrift.Play();
        }
        else if (!isDrifting && carDrift.isPlaying)
        {
            carDrift.Stop();
        }
  }
}
