using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Transform carDisplayPoint;

    private int selectedCarIndex = 0;
    private GameObject currentCar;

  void Start()
  {
    ShowCar(selectedCarIndex);
  }
  public void ShowCar(int index)
    {
        if (currentCar != null)
        {
            Destroy(currentCar);
        }
        selectedCarIndex = index;
        currentCar = Instantiate(cars[selectedCarIndex], carDisplayPoint.position, carDisplayPoint.rotation);
    }

    public void NextCar()
    {
        selectedCarIndex = (selectedCarIndex + 1) % cars.Length;
        ShowCar(selectedCarIndex);
    }

    public void PreviousCar()
    {
        selectedCarIndex = (selectedCarIndex - 1 + cars.Length) % cars.Length;
        ShowCar(selectedCarIndex);
    }

    public void SelectCar()
    {
        PlayerPrefs.SetInt("SelectedCar", selectedCarIndex);
        SceneManager.LoadScene("DriftRush");
    }
}
