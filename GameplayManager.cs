using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform carSpawnPoint;
    public Text armorLevelText;
    public Text coinText;

    void Start()
    {
        Debug.Log("GameplayManager Start");
        int selectedCarIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);
        SetInitialCoinText();
        if (selectedCarIndex >= 0 && selectedCarIndex < carPrefabs.Length)
        {
            Quaternion carRotation = Quaternion.Euler(0, -270, 0);
            GameObject carInstance = Instantiate(carPrefabs[selectedCarIndex], carSpawnPoint.position, carRotation);
            carInstance.transform.SetParent(carSpawnPoint, false);

            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(carInstance.transform);
            }
            else
            {
                Debug.LogError("CameraFollow script not found in the scene!");
            }

            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            if (enemySpawner != null)
            {
                enemySpawner.SetPlayerTarget(carInstance.transform);
            }
            else
            {
                Debug.LogError("EnemySpawner script not found in the scene!");
            }

            CoinCollector coinCollector = carInstance.GetComponent<CoinCollector>();
            if (coinCollector != null && coinText != null)
            {
                coinCollector.SetCoinText(coinText);
                Debug.Log("CoinText assigned to CoinCollector");
            }
            else
            {
                Debug.LogError("CoinCollector or CoinText is not found or not assigned!");
            }

            PlayerController playerController = carInstance.GetComponent<PlayerController>();
            if (playerController != null && armorLevelText != null)
            {
                playerController.SetArmorLevelText(armorLevelText);
                Debug.Log("ArmorLevelText set for PlayerController");
            }
            else
            {
                Debug.LogError("PlayerController or ArmorLevelText is not found or not assigned!");
            }

        }
        else
        {
            Debug.LogError("Selected car index is out of range");
        }
    }

    private void SetInitialCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "0";
            Debug.Log("Initial coinText set to 0.");
        }
        else
        {
            Debug.LogError("coinText is not assigned in the inspector!");
        }
    }
}
