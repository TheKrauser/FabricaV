using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Credits : MonoBehaviour
{
    [SerializeField] private Transform anchor;
    [SerializeField] private Transform credits;

    private Transform pause;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Pause") !=  null)
            pause = GameObject.FindGameObjectWithTag("Pause").transform;

        if (pause != null)
        {
            Destroy(pause.gameObject);
        }

        RollCredits();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadingScene.Instance.LoadScene("Menu");
        }
    }

    private void RollCredits()
    {
        credits.DOMove(anchor.position, 150f).SetUpdate(true);
    }
}
