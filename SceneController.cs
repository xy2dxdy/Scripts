using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        LoadScene("MainMenu");
    }

    public void LoadGameplay()
    {
        LoadScene("Gameplay");
    }
    public void LoadUpgradeShop()
    {
        SceneManager.LoadScene("UpgradeShop");
    }
    public void LoadPlanetScene()
    {
        SceneManager.LoadScene("MenuPlanet");
    }
    public void LoadCarSelection()
    {
        SceneManager.LoadScene("CarSelection");
    }
}
