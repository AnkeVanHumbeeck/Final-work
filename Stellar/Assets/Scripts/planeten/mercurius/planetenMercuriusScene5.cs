using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class planetenMercuriusScene5 : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject textBox;

    [SerializeField] AudioSource VoAlgemeen;
    [SerializeField] AudioSource SFXAlgemeen;
    [SerializeField] AudioMixer myMixer;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;

    [SerializeField] CanvasGroup nextButtonCanvasGroup;

    [SerializeField] CanvasGroup previousButtonCanvasGroup;


    void Update()
    {
        textLength = TextCreator.charCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        ApplySavedVolumes();

        StartCoroutine(EventStarter());
    }

    void ApplySavedVolumes()
    {
        float voVol = PlayerPrefs.GetFloat("VOVolume", 0.75f);
        myMixer.SetFloat("VO", Mathf.Log10(voVol) * 20);

        float SFXVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        myMixer.SetFloat("SFX", Mathf.Log10(SFXVol) * 20);
    }

    IEnumerator EventStarter()
    {
        // Zet de knop uit en half transparant
        nextButtonCanvasGroup.alpha = 0.5f;
        nextButtonCanvasGroup.interactable = false;
        nextButtonCanvasGroup.blocksRaycasts = false;

        previousButtonCanvasGroup.alpha = 0.5f;
        previousButtonCanvasGroup.interactable = false;
        previousButtonCanvasGroup.blocksRaycasts = false;

        mainTextObject.SetActive(true);
        textToSpeak = "Mercurius is dus een planeet met extreme temperaturen en geen lucht, maar het blijft een bijzondere wereld die we van de aarde kunnen bewonderen.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        VoAlgemeen.Play();
        SFXAlgemeen.Play();


        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);

        textBox.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        fadeScreenIn.SetActive(false);

        // Zet de knop volledig aan en zichtbaar
        nextButtonCanvasGroup.alpha = 1f;
        nextButtonCanvasGroup.interactable = true;
        nextButtonCanvasGroup.blocksRaycasts = true;

        previousButtonCanvasGroup.alpha = 1f;
        previousButtonCanvasGroup.interactable = true;
        previousButtonCanvasGroup.blocksRaycasts = true;


    }

    public void nextScene()
    {
        SceneManager.LoadScene("planeten-mercurius-quiz1");
    }

    public void previousScene()
    {
        SceneManager.LoadScene("planeten-mercurius-scene4");
    }
}
