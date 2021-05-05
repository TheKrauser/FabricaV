using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookManager : MonoBehaviour
{
    public AutoFlip flip;
    public BookUI bookUI;
    public Transform bookToOpen;
    public Transform anchorCenter;
    public Transform anchorDown;

    public bool isOpen = false;

    public void OpenBook(FirstPersonAIO firstPerson)
    {
        if (!isOpen)
        {
            isOpen = !isOpen;
            SetCanFlip(bookUI, false);
            ChangeMovement(firstPerson, false);
            AudioManager.Instance.PlaySoundEffect("Open_Book");
            bookToOpen.gameObject.SetActive(true);
            bookToOpen.DOMove(anchorCenter.position, 0.6f).OnComplete(() => SetCanFlip(bookUI, true));
        }
    }

    public void CloseBook(FirstPersonAIO firstPerson)
    {
        if (isOpen && !flip.isFlipping)
        {
            AudioManager.Instance.PlaySoundEffect("Close_Book");
            SetCanFlip(bookUI, false);
            bookToOpen.DOMove(anchorDown.position, 0.6f).OnComplete(() =>
            {
                isOpen = !isOpen;
                bookToOpen.gameObject.SetActive(false);
                ChangeMovement(firstPerson, true);
            });
        }
    }

    private void ChangeMovement(FirstPersonAIO firstPerson, bool enable)
    {
        firstPerson.enableCameraMovement = enable;
        firstPerson.playerCanMove = enable;
    }

    private void SetCanFlip(BookUI bookUI, bool trueOrFalse)
    {
        bookUI.canFlip = trueOrFalse;
    }
}
