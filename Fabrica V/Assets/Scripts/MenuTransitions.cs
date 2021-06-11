using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuTransitions : MonoBehaviour
{
    public static Transform activeOption, oldOption;
    public static MenuTransitions Instance;

    private Transform pause;

    void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Pause") != null)
            pause = GameObject.FindGameObjectWithTag("Pause").transform;

        if (pause != null)
        Destroy(pause.gameObject);

    }

    public void Transition(Transform disable, Transform enable)
    {
        disable.gameObject.SetActive(false);
        enable.gameObject.SetActive(true);
    }

}
