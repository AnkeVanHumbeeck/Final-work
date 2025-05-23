using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Zonsverduistering_Completed", 1);
        PlayerPrefs.Save();
    }
}
