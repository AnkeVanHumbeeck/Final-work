using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicAlgemeen;
    [SerializeField] AudioMixer myMixer;

    private static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
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

}