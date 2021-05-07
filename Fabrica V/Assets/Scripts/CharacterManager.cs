using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    FirstPersonAIO firstPerson;
    Camera playerCamera;
    private OutlineShader selectionOutline;
    private OutlineShader oldSelection;

    private void Awake()
    {
        firstPerson = GetComponent<FirstPersonAIO>();
    }

    void Start()
    {
        playerCamera = firstPerson.playerCamera;
    }

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            Transform selection = hit.transform;

            if (selection != null && selection.GetComponent<OutlineShader>() != null)
            {
                selectionOutline = selection.GetComponent<OutlineShader>();
                selectionOutline.enabled = true;
                oldSelection = selectionOutline;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (selection.GetComponent<BookManager>() != null)
                    {
                        var book = selection.GetComponent<BookManager>();
                        book.OpenBook(firstPerson);
                    }

                    else if (selection.GetComponent<Door_Config>() != null)
                    {
                        var doorScript = selection.GetComponent<Door_Config>();
                        var anim = selection.GetComponent<Animator>();
                        bool door = anim.GetBool("Door");
                        if (door)
                        {
                            doorScript.CloseDoor();
                        }
                        else
                        {
                            doorScript.OpenDoor();
                        }
                    }

                    else
                        return;
                }

                if (Input.GetKeyDown(KeyCode.R) && selection.GetComponent<BookManager>() != null)
                {
                    var book = selection.GetComponent<BookManager>();
                    var bookUI = selection.GetComponent<BookUI>();
                    book.CloseBook(firstPerson);
                }
            }

            else
            {
                //selection = null;
                if (selectionOutline != null || selection == null)
                {
                    selectionOutline.enabled = false;
                    oldSelection.enabled = false;
                }
                else
                    return;
            }
        }
        else
        {
            if (selectionOutline != null)
            {
                selectionOutline.enabled = false;
                oldSelection.enabled = false;
            }
        }
    }
}
