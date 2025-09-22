using UnityEngine;

public class LapManager : MonoBehaviour
{
    public static LapManager Instance;

    [Header("Lap Settings")]
    [SerializeField] private CheckpointIdx[] checkpoints;
    [SerializeField] private int lastCheckpointIdx = -1;
    [SerializeField] private bool isCircuit = false;
    [SerializeField] private int totalLaps = 1;
    private float totalTime = 0f;
    private int currentLap = 1;
    private bool raceStarted = false;
    private bool raceFinished = false;

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

    void Update()
    {
        if (raceStarted)
        {
            totalTime += Time.deltaTime;
        }
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

        if (!isCircuit && lastCheckpointIdx == checkpoints.Length - 1 && checkpointIdx == 0)
        {
            EndRace();
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
        totalTime = 0f;
        raceStarted = true;
        raceFinished = false;
    }

    private void EndRace()
    {
        Debug.Log("Ended" + totalTime.ToString("F2") + " seconds");

        PhysicsCarMovement.Instance.StartBraking();

        raceFinished = true;
        raceStarted = false;

        PhysicsCarMovement.Instance.OnCarStopped += HandleCarStopped;
    }

    private void HandleCarStopped()
    {

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowEndRacePanel();
        }
        Time.timeScale = 0f;

        PhysicsCarMovement.Instance.OnCarStopped -= HandleCarStopped;
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }

    public int GetTotalLaps()
    {
        return totalLaps;
    }
    public float GetTotalTime()
    {
        return totalTime;
    }
}
