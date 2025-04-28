using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopManager : MonoBehaviour
{
    public Text plasmaText;
    public Text speedLevelText;
    public Text armorLevelText;
    public Button speedUpgradeButton;
    public Button armorUpgradeButton;
    public int speedUpgradeCost = 20;
    public int armorUpgradeCost = 30;
    public GameObject[] carPrefabs;
    private int selectedCarIndex;
    private Car currentCar;

    void Start()
    {
        selectedCarIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        if (speedUpgradeButton == null || armorUpgradeButton == null || plasmaText == null || speedLevelText == null || armorLevelText == null)
        {
            Debug.LogError("One or more UI elements are not assigned in the inspector!");
            return;
        }

        if (carPrefabs == null || carPrefabs.Length == 0)
        {
            Debug.LogError("Car prefabs array is null or empty!");
            return;
        }

        if (selectedCarIndex >= 0 && selectedCarIndex < carPrefabs.Length)
        {
            GameObject carPrefab = carPrefabs[selectedCarIndex];
            if (carPrefab != null)
            {
                currentCar = carPrefab.GetComponent<Car>();
                if (currentCar == null)
                {
                    Debug.LogError("Car component not found on the car prefab");
                    return;
                }
            }
            else
            {
                Debug.LogError("Car prefab is null");
                return;
            }

            UpdateUI();
            speedUpgradeButton.onClick.AddListener(() => PurchaseUpgrade("speed"));
            armorUpgradeButton.onClick.AddListener(() => PurchaseUpgrade("armor"));
        }
        else
        {
            Debug.LogError("Selected car index is out of range");
        }
    }

    void PurchaseUpgrade(string upgradeType)
    {
        if (currentCar == null) return;

        switch (upgradeType)
        {
            case "speed":
                if (PlasmaManager.Instance.totalPlasma >= speedUpgradeCost)
                {
                    PlasmaManager.Instance.SpendPlasma(speedUpgradeCost);
                    currentCar.speed += 2;
                    PlayerPrefs.SetFloat("CarSpeed" + currentCar.name, currentCar.speed);
                }
                break;
            case "armor":
                if (PlasmaManager.Instance.totalPlasma >= armorUpgradeCost)
                {
                    PlasmaManager.Instance.SpendPlasma(armorUpgradeCost);
                    currentCar.armor += 1;
                    PlayerPrefs.SetInt("CarArmor" + currentCar.name, currentCar.armor);
                }
                break;
        }
        UpdateUI();
    }

    private void UpdatePlasmaText()
    {
        int currentPlasma = PlayerPrefs.GetInt("Plasma", 0);
        plasmaText.text = currentPlasma.ToString();
    }

    void UpdateUI()
    {
        if (currentCar != null)
        {
            if (speedLevelText != null)
            {
                speedLevelText.text = "Speed Level: " + currentCar.speed;
            }
            else
            {
                Debug.LogError("speedLevelText is null in UpdateUI");
            }

            if (armorLevelText != null)
            {
                armorLevelText.text = "Armor Level: " + currentCar.armor;
            }
            else
            {
                Debug.LogError("armorLevelText is null in UpdateUI");
            }

            if (speedUpgradeButton != null)
            {
                Text speedButtonText = speedUpgradeButton.GetComponentInChildren<Text>();
                if (speedButtonText != null)
                {
                    speedButtonText.text = "Speed Upgrade (" + speedUpgradeCost + " Plasma)";
                }
                else
                {
                    Debug.LogError("speedUpgradeButton text component is null in UpdateUI");
                }
            }
            else
            {
                Debug.LogError("speedUpgradeButton is null in UpdateUI");
            }

            if (armorUpgradeButton != null)
            {
                Text armorButtonText = armorUpgradeButton.GetComponentInChildren<Text>();
                if (armorButtonText != null)
                {
                    armorButtonText.text = "Armor Upgrade (" + armorUpgradeCost + " Plasma)";
                }
                else
                {
                    Debug.LogError("armorUpgradeButton text component is null in UpdateUI");
                }
            }
            else
            {
                Debug.LogError("armorUpgradeButton is null in UpdateUI");
            }

            UpdatePlasmaText();
        }
        else
        {
            Debug.LogError("currentCar is null in UpdateUI");
        }
    }
}
               