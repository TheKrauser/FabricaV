using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnd : MonoBehaviour
{

    public void EndGame()
    {
        LoadingScene.Instance.LoadScene("Credits");
    }
}
