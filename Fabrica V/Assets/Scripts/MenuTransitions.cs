using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuTransitions : MonoBehaviour
{
    public static Transform activeOption, oldOption;
    public static MenuTransitions Instance;

    void Awake()
    {
        Instance = this;

    }

    void Start()
    {

    }

    public void Transition(Transform disable, Transform enable)
    {
        disable.gameObject.SetActive(false);
        enable.gameObject.SetActive(true);
    }

}
