using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ImageGuessGame : MonoBehaviour
{
    public Image imageDisplay;
    public TMP_InputField inputField;
    public TextMeshProUGUI feedbackText;

    [System.Serializable]
    public class ImageQuestion
    {
        public Sprite image;
        public string answer;
    }

    public List<ImageQuestion> questions;
    private int currentQuestion = 0;

    void Start()
    {
        ShuffleQuestions();
        if (questions.Count > 3)
        {
            questions = questions.GetRange(0, 3);
        }
        inputField.onSubmit.AddListener(delegate { CheckAnswer(); });
        ShowNextQuestion();
    }

    void Update()
    {
        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
            inputField.Select();
        }
    }

    public void CheckAnswer()
    {
        string userAnswer = inputField.text.Trim().ToLower();
        string correctAnswer = questions[currentQuestion].answer.ToLower();

        if (userAnswer == correctAnswer)
        {
            feedbackText.text = "Juist!";
            currentQuestion++;

            if (currentQuestion < questions.Count)
            {
                ShowNextQuestion();
            }
            else
            {
                feedbackText.text = "Goed zo!";
                Invoke("ReturnToMenu", 2f);
            }
        }
        else
        {
            string firstLetter = questions[currentQuestion].answer.Substring(0, 1).ToUpper();
            feedbackText.text = $"Probeer opnieuw! Hint: Het woord start met '{firstLetter}'";
        }
        inputField.text = "";
    }

    void ShowNextQuestion()
    {
        inputField.text = "";
        inputField.ActivateInputField();
        inputField.Select();

        imageDisplay.sprite = questions[currentQuestion].image;
        feedbackText.text = "";
    }

    void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            int randomIndex = Random.Range(i, questions.Count);
            var temp = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }
    void ReturnToMenu()
    {
        SceneManager.LoadScene("Sterren");
    }
}
