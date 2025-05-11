using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text totalCoinsText;
    public Text plasmaText;

    private void Start()
    {
        UpdateTotalCoinsText();
        UpdatePlasmaText();
    }

    public void UpdateTotalCoinsText()
    {
        if (CoinManager.Instance != null)
        {
            totalCoinsText.text = CoinManager.Instance.totalCoins.ToString();
        }
        else
        {
            totalCoinsText.text = "0";
        }
    }
    private void UpdatePlasmaText()
    {
        int currentPlasma = PlayerPrefs.GetInt("Plasma", 0);
        plasmaText.text = currentPlasma.ToString(); 
    }
        private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateTotalCoinsText();
    }
}
