using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuizManagerProxima3 : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject textBox;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;

    [SerializeField] CanvasGroup nextButtonCanvasGroup;
    [SerializeField] CanvasGroup previousButtonCanvasGroup;

    [Header("Quiz-elementen")]
    //[SerializeField] Image vraagAfbeelding;
    [SerializeField] Button[] antwoordKnoppen;
    [SerializeField] TMP_Text[] antwoordTeksten;
    [SerializeField] GameObject feedbackObject;
    [SerializeField] TMP_Text feedbackTekst;

    [SerializeField] string vraag = "Hoe lang doet de planeet over een rondje om Proxima Centauri?";
    [SerializeField] Sprite vraagSprite;
    [SerializeField] string[] antwoorden = new string[4];
    [SerializeField] int correctIndex = 1;

    private int vraagIndex;

    private bool heeftGeantwoord = false;

    void Start()
    {
        SetVraagIndex();
        StartCoroutine(EventStarter());

        StartCoroutine(EventStarter());
    }

    void SetVraagIndex()
    {
        string sceneName = SceneManager.GetActiveScene().name;


        if (sceneName.Contains("quiz"))
        {
            string nummerStr = System.Text.RegularExpressions.Regex.Match(sceneName, @"\d+").Value;
            if (int.TryParse(nummerStr, out int nummer))
            {
                vraagIndex = nummer - 1;
            }
        }
    }


    IEnumerator EventStarter()
    {

        mainTextObject.SetActive(true);

        nextButtonCanvasGroup.alpha = 0.5f;
        nextButtonCanvasGroup.interactable = false;

        previousButtonCanvasGroup.alpha = 0.5f;
        previousButtonCanvasGroup.interactable = false;

        textToSpeak = vraag;
        textBox.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        //vraagAfbeelding.sprite = vraagSprite;
        feedbackObject.SetActive(false);

        for (int i = 0; i < antwoordKnoppen.Length; i++)
        {
            antwoordKnoppen[i].interactable = true;
            antwoordKnoppen[i].GetComponent<Image>().color = Color.white;
            antwoordTeksten[i].text = antwoorden[i];

            int index = i;
            antwoordKnoppen[i].onClick.RemoveAllListeners();
            antwoordKnoppen[i].onClick.AddListener(() => Beantwoord(index));
        }

        yield return new WaitUntil(() => TextCreator.charCount == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        fadeScreenIn.SetActive(false);


        if (QuizStateManager.Instance.heeftGeantwoord[vraagIndex])
        {
            Beantwoord(QuizStateManager.Instance.gekozenAntwoorden[vraagIndex]);
        }
    }

    void Beantwoord(int gekozenIndex)
    {
        if (heeftGeantwoord) return;
        heeftGeantwoord = true;

        QuizStateManager.Instance.heeftGeantwoord[vraagIndex] = true;
        QuizStateManager.Instance.gekozenAntwoorden[vraagIndex] = gekozenIndex;

        if (gekozenIndex == correctIndex)
        {
            QuizStateManager.Instance.aantalJuisteAntwoorden++;
            Debug.Log("Aantal juiste antwoorden: " + QuizStateManager.Instance.aantalJuisteAntwoorden);

            if (QuizStateManager.Instance.aantalJuisteAntwoorden == 3)
            {
                PlayerPrefs.SetInt("ProximaCentauri_Completed", 1);
                PlayerPrefs.Save();
                Debug.Log("Alle drie de vragen goed — voltooid!");
            }
        }


        Color groen, rood;
        ColorUtility.TryParseHtmlString("#507E2E", out groen);
        ColorUtility.TryParseHtmlString("#C7303E", out rood);

        for (int i = 0; i < antwoordKnoppen.Length; i++)
        {
            antwoordKnoppen[i].interactable = false;

            if (i == correctIndex)
                antwoordKnoppen[i].GetComponent<Image>().color = groen;
            else if (i == gekozenIndex)
                antwoordKnoppen[i].GetComponent<Image>().color = rood;
        }

        if (gekozenIndex != correctIndex)
        {
            feedbackObject.SetActive(true);
            feedbackTekst.text = $"Het juiste antwoord was: {antwoorden[correctIndex]}";
        }

        nextButtonCanvasGroup.alpha = 1f;
        nextButtonCanvasGroup.interactable = true;
        previousButtonCanvasGroup.alpha = 1f;
        previousButtonCanvasGroup.interactable = true;
    }

    public void nextScene()
    {
        if (QuizStateManager.Instance.aantalJuisteAntwoorden == 3)
        {

            SceneManager.LoadScene("Gallerij Sterren");
        }
        else
        {

            SceneManager.LoadScene("Sterren");
        }
    }

    public void previousScene()
    {
        SceneManager.LoadScene("sterren-proxima-quiz2");
    }
}
