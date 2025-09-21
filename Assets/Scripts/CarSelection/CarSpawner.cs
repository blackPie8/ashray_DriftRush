using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("SelectedCar", 0);
        GameObject car = Instantiate(carPrefabs[selectedCar], spawnPoint.position, spawnPoint.rotation);
  }
}
