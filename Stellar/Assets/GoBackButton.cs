using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackButton : MonoBehaviour
{
    [SerializeField] private string fallbackScene = "Main menu";

    public void GoBack()
    {
        string previous = SceneTracker.GetPreviousNonGalleryScene();
        if (!string.IsNullOrEmpty(previous))
        {
            SceneManager.LoadScene(previous);
        }
        else
        {
            Debug.LogWarning("No valid previous scene found. Using fallback.");
            SceneManager.LoadScene(fallbackScene);
        }
    }
}
