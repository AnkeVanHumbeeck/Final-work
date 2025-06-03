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
        // Zon
        PlayerPrefs.SetInt("Zonsverduistering_Completed", 0);
        PlayerPrefs.SetInt("Zon_Completed", 0);
        // Maan
        PlayerPrefs.SetInt("OntstaanMaan_Completed", 0);
        PlayerPrefs.SetInt("Maansverduistering_Completed", 0);
        PlayerPrefs.SetInt("Maan_Completed", 0);
        PlayerPrefs.SetInt("Maanfases_Completed", 0);
        PlayerPrefs.SetInt("WatIsDeMaan_Completed", 0);
        // Planeten
        PlayerPrefs.SetInt("SoortenPlaneten_Completed", 0);
        PlayerPrefs.SetInt("Mercurius_Completed", 0);
        PlayerPrefs.SetInt("Venus_Completed", 0);
        PlayerPrefs.SetInt("Aarde_Completed", 0);
        PlayerPrefs.SetInt("Mars_Completed", 0);
        PlayerPrefs.SetInt("Jupiter_Completed", 0);
        PlayerPrefs.SetInt("Saturnus_Completed", 0);
        PlayerPrefs.SetInt("Neptunus_Completed", 0);
        PlayerPrefs.SetInt("Uranus_Completed", 0);
        PlayerPrefs.SetInt("Pluto_Completed", 0);
        // Sterren
        PlayerPrefs.SetInt("ProximaCentauri_Completed", 0);
        PlayerPrefs.SetInt("Sterrenstelsel_Completed", 0);
        PlayerPrefs.SetInt("Sterrenbeeld_Completed", 0);
        PlayerPrefs.SetInt("LevenVanEenSter_Completed", 0);
        // Rotsen
        PlayerPrefs.SetInt("Asteroide_Completed", 0);
        PlayerPrefs.SetInt("VallendeSter_Completed", 0);
        PlayerPrefs.SetInt("Komeet_Completed", 0);
        PlayerPrefs.SetInt("RotsenAlgemeen_Completed", 0);
        PlayerPrefs.SetInt("Meteoriet_Completed", 0);

        PlayerPrefs.Save();
    }
}
