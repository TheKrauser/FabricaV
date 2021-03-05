using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    FirstPersonAIO firstPerson;
    Camera playerCamera;
    private Outline selectionOutline;
    public Image button;

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
        if (Physics.Raycast(ray, out hit, 2))
        {
            Transform selection = hit.transform;

            if (selection.GetComponent<Outline>() != null)
            {
                selectionOutline = selection.GetComponent<Outline>();
                selectionOutline.enabled = true;
                button.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                    SceneManager.LoadScene("Game1");
            }

            else
            {
                if (selectionOutline != null)
                {
                    selectionOutline.enabled = false;
                    button.enabled = false;
                }
                else
                    return;
            }
        }
    }
}
