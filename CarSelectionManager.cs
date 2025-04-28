using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform carSpawnPoint;
    public Button startGameButton;
    public Button leftButton;
    public Button rightButton;
    public Text coinText;
    public Text plasmaText;

    private int selectedCarIndex = 0;
    private GameObject currentCarInstance;

    void Start()
    {
        if (carSpawnPoint == null || startGameButton == null || leftButton == null || rightButton == null || carPrefabs == null || carPrefabs.Length == 0 || coinText == null || plasmaText == null)
        {
            return;
        }

        UpdateCarDisplay();
        UpdateCurrencyDisplay();
        startGameButton.onClick.AddListener(StartGame);
        leftButton.onClick.AddListener(SelectPreviousCar);
        rightButton.onClick.AddListener(SelectNextCar);
    }

    public void SelectNextCar()
    {
        selectedCarIndex = (selectedCarIndex + 1) % carPrefabs.Length;
        UpdateCarDisplay();
    }

    public void SelectPreviousCar()
    {
        selectedCarIndex = (selectedCarIndex - 1 + carPrefabs.Length) % carPrefabs.Length;
        UpdateCarDisplay();
    }

    private void UpdateCarDisplay()
    {
        if (currentCarInstance != null)
        {
            Destroy(currentCarInstance);
        }

        if (carPrefabs[selectedCarIndex] != null)
        {
            currentCarInstance = Instantiate(carPrefabs[selectedCarIndex], carSpawnPoint.position, carSpawnPoint.rotation, carSpawnPoint);

            Rigidbody rb = currentCarInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
    }


    private void UpdateCurrencyDisplay()
    {
        int currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        int currentPlasma = PlayerPrefs.GetInt("Plasma", 0);

        coinText.text = currentCoins.ToString();
        plasmaText.text = currentPlasma.ToString();
    }

    private void StartGame()
    {
        PlayerPrefs.SetInt("SelectedCarIndex", selectedCarIndex);
        SceneManager.LoadScene("Gameplay");
    }
   
}
