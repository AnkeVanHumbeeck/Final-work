using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneTracker.RecordScene(currentScene);
        SceneManager.LoadScene(sceneName);
    }
}
