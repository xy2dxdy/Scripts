using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text totalCoinsText; // Ссылка на UI текст для отображения общего количества монет
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
        int currentPlasma = PlayerPrefs.GetInt("Plasma", 0); // Загрузка количества плазмы из PlayerPrefs
        plasmaText.text = currentPlasma.ToString(); 
    }
        private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Подписка на событие загрузки сцены
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Отписка от события загрузки сцены
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateTotalCoinsText(); // Обновление текста при загрузке сцены
    }
}
