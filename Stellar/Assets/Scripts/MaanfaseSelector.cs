using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MoonPhaseGame : MonoBehaviour
{
    public Sprite[] moonPhaseImages;
    private string[] moonPhaseNames = new string[]
    {
        "Nieuwe maan",
        "Wassende halve maan",
        "Eerste kwartier",
        "Wassende maan",
        "Volle maan",
        "Afnemende maan",
        "Laatste kwartier",
        "Afnemende halve maan"
    };

    public Image targetMoonImage;
    public TMP_Text targetPhaseNameText;
    public Image displayedMoonImage;
    public Slider phaseSlider;
    public Button submitButton;
    public TMP_Text resultText;
    public string menuSceneName = "Maan";
    private int targetPhase;
    private int currentQuestion = 0;
    private int totalQuestions = 3;
    private List<int> availablePhases;

    void Start()
    {
        phaseSlider.wholeNumbers = true;
        phaseSlider.minValue = 0;
        phaseSlider.maxValue = 7;

        phaseSlider.onValueChanged.AddListener(UpdateMoonImage);
        submitButton.onClick.AddListener(OnSubmit);

        // Maak lijst met beschikbare fases (0–7)
        availablePhases = new List<int>();
        for (int i = 0; i < moonPhaseImages.Length; i++)
        {
            availablePhases.Add(i);
        }

        StartNewQuestion();
    }

    void StartNewQuestion()
    {
        // Kies een unieke fase
        int randomIndex = Random.Range(0, availablePhases.Count);
        targetPhase = availablePhases[randomIndex];
        availablePhases.RemoveAt(randomIndex);

        targetMoonImage.sprite = moonPhaseImages[targetPhase];
        targetPhaseNameText.text = moonPhaseNames[targetPhase];
        UpdateMoonImage(phaseSlider.value);
        resultText.text = "";
    }

    void UpdateMoonImage(float value)
    {
        int index = Mathf.RoundToInt(value);
        displayedMoonImage.sprite = moonPhaseImages[index];
    }

    void OnSubmit()
    {
        submitButton.interactable = false;

        int selectedPhase = Mathf.RoundToInt(phaseSlider.value);
        if (selectedPhase == targetPhase)
        {
            resultText.text = "Juist!";
        }
        else
        {
            resultText.text = "Probeer opnieuw!";
        }

        currentQuestion++;
        StartCoroutine(NextQuestionAfterDelay());
    }

    IEnumerator NextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (currentQuestion >= totalQuestions)
        {
            SceneManager.LoadScene(menuSceneName);
        }
        else
        {
            StartNewQuestion();
            submitButton.interactable = true;
        }
    }
}
