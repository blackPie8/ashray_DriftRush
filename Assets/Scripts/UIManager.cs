using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] TextMeshProUGUI speedValue;
    [SerializeField] TextMeshProUGUI currentLap;
    [SerializeField] TextMeshProUGUI raceTime;
    [SerializeField] TextMeshProUGUI displayTime;
    [SerializeField] private GameObject Panel;
    private PhysicsCarMovement carMovement;
    private float speed = 0f;
    private int currentLapVal;
    private int totalLaps;
    private float totalTime = 0f;

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
        Panel.SetActive(false);

        if (carMovement == null)
        {
            carMovement = FindAnyObjectByType<PhysicsCarMovement>();
        }

    }
    void Update()
    {
        SpeedUI();
        LapUI();
        TimeUI();
    }

    void SpeedUI()
    {
        speed = carMovement.CarSpeed();
        speedValue.text = speed.ToString("0" + "km/h");
    }

    void LapUI()
    {
        totalLaps = LapManager.Instance.GetTotalLaps();
        currentLapVal = LapManager.Instance.GetCurrentLap();

        currentLap.text = currentLapVal.ToString() + " / " + totalLaps.ToString();
    }

    void TimeUI()
    {
        totalTime = LapManager.Instance.GetTotalTime();
        raceTime.text = totalTime.ToString("F2") + "sec";
        displayTime.text = totalTime.ToString("F2") + "sec";
    }

    public void ShowEndRacePanel()
    {
        Panel.SetActive(true);
    }
}
