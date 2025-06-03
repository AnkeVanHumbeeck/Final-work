using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicAlgemeen;
    [SerializeField] AudioMixer myMixer;

    private static MusicManager instance;

    [SerializeField] private string[] allowedScenes;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (System.Array.IndexOf(allowedScenes, scene.name) == -1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ApplySavedVolumes();
    }

    void ApplySavedVolumes()
    {
        float MusicVol = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        myMixer.SetFloat("Music", Mathf.Log10(MusicVol) * 20);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}