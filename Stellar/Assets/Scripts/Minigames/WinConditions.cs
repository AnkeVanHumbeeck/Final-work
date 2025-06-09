using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Drag[] items;
    public TMP_Text resultText;

    [Tooltip("Name of the scene to load after winning.")]
    public string nextSceneName = "Sterren-vragen";

    public void CheckForWin()
    {
        foreach (Drag item in items)
        {
            if (!item.locked)
                return;
        }
        resultText.text = "Proficiat! Alles is juist";

        StartCoroutine(WinAndLoadNextScene());
    }

    private IEnumerator WinAndLoadNextScene()
    {
        yield return new WaitForSeconds(2f);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set.");
        }
    }
}
