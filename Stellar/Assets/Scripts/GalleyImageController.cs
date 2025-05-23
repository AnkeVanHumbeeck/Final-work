using UnityEngine;
using UnityEngine.UI;
using TMPro; // Optional, if you're using TextMeshPro

public class GalleryImageController : MonoBehaviour
{
    public Material baseMaterial; // Grayscale-capable material
    public string levelKey; // e.g., "Level1_Completed"
    public GameObject levelTextObject; // Drag your child text GameObject here

    private void Start()
    {
        bool levelCompleted = PlayerPrefs.GetInt(levelKey, 0) == 1;

        // Clone material to avoid shared updates
        Material matInstance = Instantiate(baseMaterial);
        matInstance.SetFloat("_IsGrayscale", levelCompleted ? 0f : 1f);
        GetComponent<Image>().material = matInstance;

        // Show/hide text
        if (levelTextObject != null)
        {
            levelTextObject.SetActive(levelCompleted);
        }
    }
}
