using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Transform carDisplayPoint;
    private float rotationSpeed = 50f;
    private int selectedCarIndex = 0;
    private GameObject currentCar;

    void Start()
    {
        ShowCar(selectedCarIndex);
    }

    void Update()
    {
        if (currentCar != null)
        {
            currentCar.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
    public void ShowCar(int index)
    {
        if (currentCar != null)
        {
            Destroy(currentCar);   // DestroyImmediate(currentCar);  - not runtime safe
            currentCar = null;
        }

        selectedCarIndex = index;
        StartCoroutine(SpawnCarNextFrame());
        // currentCar = Instantiate(cars[selectedCarIndex], carDisplayPoint.position, carDisplayPoint.rotation);
    }

    private IEnumerator SpawnCarNextFrame()
    {
        yield return null;
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
