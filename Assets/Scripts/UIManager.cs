using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedValue;
    [SerializeField] PhysicsCarMovement carMovement;
    private float speed = 0f;

    void Update()
    {
        SpeedUI();
    }

    void SpeedUI()
    {
        speed = carMovement.CarSpeed();
        speedValue.text = speed.ToString("0" + "km/h");
    }
}
