using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookUI : MonoBehaviour
{
    [SerializeField] private AutoFlip bookFlip;
    [SerializeField] private Book book;

    public LoadingScene loading;
    private bool isLoading;
    [HideInInspector] public bool canFlip;
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        isLoading = false;
        canFlip = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && canFlip)
        {
            bookFlip.FlipLeftPage(this);
        }
        if (Input.GetKeyDown(KeyCode.D) && canFlip)
        {
            bookFlip.FlipRightPage(this);
        }
        if (Input.GetKeyDown(KeyCode.F) && book.currentPage == book.bookPages.Length)
        {
            if (!isLoading)
            {
                isLoading = true;
                AudioManager.Instance.PlaySoundEffect("Scene");
                loading.LoadScene(sceneToLoad);
            }
        }

    }
}
