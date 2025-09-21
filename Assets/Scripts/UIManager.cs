using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedValue;
    [SerializeField] TextMeshProUGUI currentLap;
    private PhysicsCarMovement carMovement;
    private float speed = 0f;
    private int currentLapVal;
    private int totalLaps;


  void Start()
  {
        if (carMovement == null)
        {
        carMovement = FindAnyObjectByType<PhysicsCarMovement>();
    }
  }
  void Update()
    {
        SpeedUI();
        LapUI();
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
}
