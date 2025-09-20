using UnityEngine;

public class CheckpointIdx : MonoBehaviour
{
    public int checkpointIdx;
  public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CarBody"))
        {
            LapManager.Instance.CheckpointReached(checkpointIdx);
        }
    }
}
 