using UnityEngine;

public class PlanetLoader : MonoBehaviour
{
    public PlanetLevelSettings[] allPlanetSettings;

    private void Start()
    {
        if (allPlanetSettings == null || allPlanetSettings.Length == 0)
        {
            Debug.LogError("PlanetLoader: �� ��������� ��������� ������ � ������ allPlanetSettings!");
            return;
        }
        int currentPlanetIndex = PlayerPrefs.GetInt("CurrentPlanetIndex", 0);
        Debug.Log("PlanetLoader: �������� ������ ������� ������� " + currentPlanetIndex);

        if (currentPlanetIndex < 0 || currentPlanetIndex >= allPlanetSettings.Length)
        {
            currentPlanetIndex = 0;
        }

        if (SceneDataManager.Instance != null)
        {
            SceneDataManager.Instance.selectedPlanetSettings = allPlanetSettings[currentPlanetIndex];
            Debug.Log("PlanetLoader: ����������� ��������� ������� " + allPlanetSettings[currentPlanetIndex].planetName);
        }
        else
        {
            Debug.LogError("PlanetLoader: SceneDataManager �� ������ � �����!");
        }
    }
}
