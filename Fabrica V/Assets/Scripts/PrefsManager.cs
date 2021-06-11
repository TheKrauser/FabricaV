using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    public static void SetConto(int conto)
    {
        PlayerPrefs.SetInt("Conto", conto);
        PlayerPrefs.Save();
    }

    public void SetCompleted(int conto)
    {
        if (conto == 1)
        {
            PlayerPrefs.SetInt("Conto1Completado", 1);
            PlayerPrefs.Save();
        }

        if (conto == 2)
        {
            PlayerPrefs.SetInt("Conto2Completado", 1);
            PlayerPrefs.Save();
        }

        if (conto == 3)
        {
            PlayerPrefs.SetInt("Conto3Completado", 1);
            PlayerPrefs.Save();
        }
    }

    public static void Sss()
    {
        PlayerPrefs.SetInt("Conto1Completado", 1);
        PlayerPrefs.SetInt("Conto2Completado", 1);
        PlayerPrefs.SetInt("Conto3Completado", 1);
        PlayerPrefs.Save();
    }
}
