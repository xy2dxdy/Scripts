using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip gameplayMusic;

    private AudioSource audioSource;
    private static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource is not initialized.");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            PlayGameplayMusic();
        }
        else
        {
            PlayBackgroundMusic();
        }
    }

    void PlayBackgroundMusic()
    {
        if (audioSource != null && audioSource.clip != backgroundMusic)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
    }

    void PlayGameplayMusic()
    {
        if (audioSource != null && audioSource.clip != gameplayMusic)
        {
            audioSource.clip = gameplayMusic;
            audioSource.Play();
        }
    }
}
