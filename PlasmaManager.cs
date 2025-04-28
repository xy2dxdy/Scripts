using UnityEngine;

public class PlasmaManager : MonoBehaviour
{
    public static PlasmaManager Instance; // Синглтон для доступа к количеству плазмы

    public int totalPlasma; // Общее количество плазмы, сохраненное на устройстве

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlasma();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlasma(int amount)
    {
        totalPlasma += amount;
        SavePlasma();
        Debug.Log("Plasma added: " + amount + ". Total Plasma: " + totalPlasma);
    }

    public void SpendPlasma(int amount)
    {
        if (totalPlasma >= amount)
        {
            totalPlasma -= amount;
            SavePlasma();
            Debug.Log("Plasma spent: " + amount + ". Remaining: " + totalPlasma);
        }
        else
        {
            Debug.Log("Not enough plasma!");
        }
    }

    private void SavePlasma()
    {
        PlayerPrefs.SetInt("Plasma", totalPlasma);
        PlayerPrefs.Save();
    }

    private void LoadPlasma()
    {
        totalPlasma = PlayerPrefs.GetInt("Plasma", 0);
        Debug.Log("Loaded Plasma: " + totalPlasma);
    }
}
