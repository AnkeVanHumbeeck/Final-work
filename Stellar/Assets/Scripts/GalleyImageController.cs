using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryImageController : MonoBehaviour
{
    public Material baseMaterial;
    public string levelKey;
    public GameObject levelTextObject;

    private void Start()
    {
        bool levelCompleted = PlayerPrefs.GetInt(levelKey, 0) == 1;

        Material matInstance = Instantiate(baseMaterial);
        matInstance.SetFloat("_IsGrayscale", levelCompleted ? 0f : 1f);
        GetComponent<Image>().material = matInstance;

        if (levelTextObject != null)
        {
            levelTextObject.SetActive(levelCompleted);
        }
    }
}
