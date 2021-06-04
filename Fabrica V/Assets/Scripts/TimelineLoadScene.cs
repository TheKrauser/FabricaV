using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineLoadScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        LoadingScene.Instance.LoadScene(sceneName);
    }
}
