using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Drag[] items;
    [Tooltip("Name of the scene to load after winning.")]
    public string nextSceneName = "Sterren-vragen";

    public void CheckForWin()
    {
        foreach (Drag item in items)
        {
            if (!item.locked)
                return;
        }

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
