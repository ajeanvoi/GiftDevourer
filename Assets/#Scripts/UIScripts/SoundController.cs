using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip getGiftSound;
    [SerializeField] private AudioClip killSound;
    [SerializeField] private AudioClip endSound;

    private AudioSource soundEffectsSource;
    private AudioSource backgroundMusicSource;

    private static SoundController instance;
    private string lastLoadedScene;
    private AudioClip currentBackgroundMusic; // Ajoutez cette variable pour stocker la musique de fond actuelle

    [SerializeField] private AudioClip musicMenu; // Musique de fond pour le menu
    [SerializeField] private AudioClip musicCredit; // Musique de fond pour les credits
    [SerializeField] private AudioClip musicGame; //  Musique de fond pour le jeu

    public static SoundController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject soundEffectsHelperObject = new GameObject("SoundEffectsHelper");
                instance = soundEffectsHelperObject.AddComponent<SoundController>();
                DontDestroyOnLoad(soundEffectsHelperObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        soundEffectsSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

        backgroundMusicSource.loop = true;
        backgroundMusicSource.playOnAwake = true;

        DontDestroyOnLoad(gameObject);

        AudioClip backgroundMusic = GetBackgroundMusicForScene(SceneManager.GetActiveScene().name);
        if (backgroundMusic != null)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.Play();
            currentBackgroundMusic = backgroundMusic; // Stockez la musique de fond actuelle
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        lastLoadedScene = SceneManager.GetActiveScene().name;
    }

    public void StopSound()
    {
        // Arrêtez tous les bruitages en cours
        soundEffectsSource.Stop();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (backgroundMusicSource == null)
            return;

        if (scene.name.Equals(lastLoadedScene))
        {
            //StopBackgroundMusic();
            //PlayBackgroundMusic();
            return;
        }

        lastLoadedScene = scene.name;

        AudioClip backgroundMusic = GetBackgroundMusicForScene(scene.name);
        if (backgroundMusic != null)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.Play();
            currentBackgroundMusic = backgroundMusic; // Stockez la musique de fond actuelle
        }
    }

    public void PlayBackgroundMusic()
    {
        if (!backgroundMusicSource.isPlaying && currentBackgroundMusic != null)
        {
            backgroundMusicSource.clip = currentBackgroundMusic; // Reprenez la musique de fond actuelle
            backgroundMusicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void MakeKillSound()
    {
        MakeSound(killSound);
    }

    public void MakeGetGiftSound()
    {
        MakeSound(getGiftSound);
    }

    public void MakeEndSound()
    {
        MakeSound(endSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        soundEffectsSource.PlayOneShot(originalClip);
    }

    private AudioClip GetBackgroundMusicForScene(string sceneName)
    {
        if (sceneName.Equals("Game"))
        {
            return musicGame;
        }
        else if (sceneName.Equals("Credit"))
        {
            return musicCredit;
        }
        else if (sceneName.Equals("Menu"))
        {
            return musicMenu;
        }
        else
        {
            return null; // Aucune musique spécifique pour cette scène
        }
    }

}

