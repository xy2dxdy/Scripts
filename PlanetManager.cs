using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Numerics;
using static UnityEditor.Progress;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour
{
    public PlanetLevelSettings[] planetSettings;


    public GameObject planetUIPrefab;
    public Transform planetContent;

    public Text plasmaText;
    public Text coinText;

    public int fixedAmount = 10;

    private int currentPlasma = 0;
    private int currentPlanetIndex = 0;
    private PlanetProgress currentProgress = new PlanetProgress();

    private void Start()
    {
        currentPlasma = PlayerPrefs.GetInt("Plasma", 0);
        currentPlanetIndex = PlayerPrefs.GetInt("CurrentPlanetIndex", 0);

        if (currentPlanetIndex < 0 || currentPlanetIndex >= planetSettings.Length)
            currentPlanetIndex = 0;

        string progressKey = "Planet" + planetSettings[currentPlanetIndex].planetName + "Coins";
        currentProgress.currentCoins = PlayerPrefs.GetInt(progressKey, 0);
        currentProgress.isComplete = PlayerPrefs.GetInt("Planet" + planetSettings[currentPlanetIndex].planetName + "Complete", 0) == 1;

        UpdatePlasmaText();
        UpdateCoinText();
        SpawnPlanetUI();
    }

    private void SpawnPlanetUI()
    {
        foreach (Transform child in planetContent)
            Destroy(child.gameObject);

        for (int i = 0; i < planetSettings.Length; i++)
        {
            PlanetLevelSettings settings = planetSettings[i];
            GameObject uiItem = Instantiate(planetUIPrefab, planetContent);
            uiItem.name = settings.planetName;

            Button planetButton = uiItem.GetComponentInChildren<Button>();
            if (planetButton != null)
            {
                if (settings.planetSprite != null)
                {
                    planetButton.image.sprite = settings.planetSprite;
                }
                else
                {
                    Debug.LogWarning("planetSprite не задан для " + settings.planetName);
                }
            }
            else
            {
                Debug.LogWarning("Button не найден в UI-элементе: " + uiItem.name);
            }

            PlanetButtonHandler handler = uiItem.GetComponentInChildren<PlanetButtonHandler>();
            if (handler != null)
            {
                handler.planetIndex = i;
                Debug.Log("Assigned planetIndex " + i + " for planet: " + settings.planetName);
            }
            else
            {
                Debug.LogWarning("PlanetButtonHandler не найден в UI-элементе: " + uiItem.name);
            }

            Slider planetSlider = uiItem.GetComponentInChildren<Slider>();
            if (planetSlider != null)
            {
                if (i < currentPlanetIndex)
                {
                    planetSlider.value = 1f;
                }
                else if (i == currentPlanetIndex)
                {
                    planetSlider.value = (float)currentProgress.currentCoins / settings.requiredCoins;
                }
                else
                {
                    planetSlider.value = 0f;
                }
            }
        }
        
    }




    public void AddFixedAmountToCurrentPlanet()
    {
        if (CoinManager.Instance.totalCoins >= fixedAmount)
        {
            CoinManager.Instance.SpendCoins(fixedAmount);
            currentProgress.currentCoins += fixedAmount;
            UpdateCoinText();

            PlanetLevelSettings settings = planetSettings[currentPlanetIndex];

            if (currentProgress.currentCoins >= settings.requiredCoins)
            {
                currentProgress.currentCoins = settings.requiredCoins;
                currentProgress.isComplete = true;

                currentPlasma += settings.plasmaReward;
                PlayerPrefs.SetInt("Plasma", currentPlasma);
                PlasmaManager.Instance.AddPlasma(settings.plasmaReward);
                PlayerPrefs.SetInt("Planet" + settings.planetName + "Complete", 1);

                if (currentPlanetIndex < planetSettings.Length - 1)
                {
                    currentPlanetIndex++;
                    string newProgressKey = "Planet" + planetSettings[currentPlanetIndex].planetName + "Coins";
                    currentProgress.currentCoins = PlayerPrefs.GetInt(newProgressKey, 0);
                    currentProgress.isComplete = false;
                    PlayerPrefs.SetInt("CurrentPlanetIndex", currentPlanetIndex);
                }
                else
                {
                    Debug.Log("Все планеты открыты!");
                }
            }

            string progressKey = "Planet" + settings.planetName + "Coins";
            PlayerPrefs.SetInt(progressKey, currentProgress.currentCoins);
            PlayerPrefs.Save();


            UpdatePlasmaText();
            UpdateCoinText();
            SpawnPlanetUI();
        }
        else
        {
            Debug.LogWarning("Недостаточно монет для вложения в планету: " + planetSettings[currentPlanetIndex].planetName);
        }
    }





    private void UpdatePlasmaText()
    {
        plasmaText.text = currentPlasma.ToString();
    }

    private void UpdateCoinText()
    {
        coinText.text = CoinManager.Instance.totalCoins.ToString();
    }

    public int GetCurrentPlanetIndex()
    {
        return currentPlanetIndex;
    }

    public void AddFixedAmountToPlanet(int planetIndex)
    {
        if (CoinManager.Instance.totalCoins >= fixedAmount)
        {
            CoinManager.Instance.SpendCoins(fixedAmount);

            if (planetIndex == currentPlanetIndex)
            {
                currentProgress.currentCoins += fixedAmount;
            }

            UpdateCoinText();

            PlanetLevelSettings settings = planetSettings[currentPlanetIndex];

            string progressKey = "Planet" + settings.planetName + "Coins";
            PlayerPrefs.SetInt(progressKey, currentProgress.currentCoins);

            if (currentProgress.currentCoins >= settings.requiredCoins)
            {
                currentProgress.currentCoins = settings.requiredCoins;
                currentProgress.isComplete = true;
                PlayerPrefs.SetInt("Planet" + settings.planetName + "Complete", 1);
            }

            PlayerPrefs.Save();

            SpawnPlanetUI();
        }
    }



}
