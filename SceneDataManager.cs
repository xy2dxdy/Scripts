using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
    public static SceneDataManager Instance;
    public PlanetLevelSettings selectedPlanetSettings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
