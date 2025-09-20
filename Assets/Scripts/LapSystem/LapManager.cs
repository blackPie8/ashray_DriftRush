using UnityEngine;

public class LapManager : MonoBehaviour
{
    public static LapManager Instance;

    [Header("Lap Settings")]
    [SerializeField] private CheckpointIdx[] checkpoints;
    [SerializeField] private int lastCheckpointIdx = -1;
    [SerializeField] private bool isCircuit = true;
    [SerializeField] private int totalLaps = 3;

    private int currentLap = 1;

    private bool raceStarted = false;
    private bool raceFinished = false;

    void Awake()
    {
        if (Instance == null)

        { Instance = this; }

    }

    public void CheckpointReached(int checkpointIdx)
    {
        if (raceFinished) return;

        if (!raceStarted && checkpointIdx != 0) return;

        if (checkpointIdx == (lastCheckpointIdx + 1) % checkpoints.Length)
        {
            UpdateCheckpoint(checkpointIdx);
        }
    }

    private void UpdateCheckpoint(int checkpointIdx)
    {
        if (!raceStarted && checkpointIdx == 0)
        {
            StartRace();
            lastCheckpointIdx = 0;
            return;
        }

        if (isCircuit && lastCheckpointIdx == checkpoints.Length - 1 && checkpointIdx == 0)
        {
            OnLapFinish();
        }

        if (checkpointIdx == (lastCheckpointIdx + 1) % checkpoints.Length)
        {
            lastCheckpointIdx = checkpointIdx;
        }
    }


    private void OnLapFinish()
    {
        currentLap++;

        if (currentLap > totalLaps)
        {
            currentLap = totalLaps;
            EndRace();
        }
    }
    private void StartRace()
    {
        raceStarted = true;
        raceFinished = false;
    }

    private void EndRace()
    {
        Debug.Log("Ended");
        raceFinished = true;
        raceStarted = false;
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }

    public int GetTotalLaps()
    {
        return totalLaps;
    }
}
