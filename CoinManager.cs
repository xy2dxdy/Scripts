using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int totalCoins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCoins();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        SaveCoins();
    }

    public void SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            SaveCoins();
            Debug.Log("Coins spent: " + amount + ". Remaining: " + totalCoins);
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }
}
