using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetGallerij : MonoBehaviour
{
    
    void Start()
    {
        WisGegevens();
    }

    public void WisGegevens()
    {
        PlayerPrefs.SetInt("Zonsverduistering_Completed", 0);
        PlayerPrefs.SetInt("Zon_Completed", 0);
        PlayerPrefs.SetInt("Maanfases_Completed", 0);
        PlayerPrefs.Save();
    }
}
