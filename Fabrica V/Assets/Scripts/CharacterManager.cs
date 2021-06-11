using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CharacterManager : MonoBehaviour
{
    FirstPersonAIO firstPerson;
    Camera playerCamera;
    private OutlineShader selectionOutline;
    private OutlineShader oldSelection;

    [SerializeField] private PlayableDirector initialCutscene;
    [SerializeField] private GameObject dialogue1, dialogue2, dialogue3, dialogue4;
    [SerializeField] private Transform livro1, livro2, livro3;
    private bool ended;

    private void Awake()
    {
        firstPerson = GetComponent<FirstPersonAIO>();
        playerCamera = firstPerson.playerCamera;

        if (PlayerPrefs.GetInt("HasSavedGame", 0) == 1)
        {
            float playerX = PlayerPrefs.GetFloat("PositionX");
            float playerY = PlayerPrefs.GetFloat("PositionY");
            float playerZ = PlayerPrefs.GetFloat("PositionZ");

            //float cameraX = PlayerPrefs.GetFloat("CameraX");
            //float cameraY = PlayerPrefs.GetFloat("CameraY");
            //float cameraZ = PlayerPrefs.GetFloat("CameraZ");

            transform.position = new Vector3(playerX, playerY, playerZ);
            //playerCamera.transform.localRotation = new Quaternion(cameraX, cameraY, cameraZ, 0);
        }
        else
        {
            initialCutscene.Play();
        }

        if (PlayerPrefs.GetInt("Conto1Completado") == 1 &&
             PlayerPrefs.GetInt("Conto2Completado") == 1 &&
                PlayerPrefs.GetInt("Conto3Completado") == 1)
        {
            ended = true;
        }
        else
            ended = false;

        if (PlayerPrefs.GetInt("Conto1Completado") == 1)
            livro1.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("Conto2Completado") == 1)
            livro2.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("Conto3Completado") == 1)
            livro3.gameObject.SetActive(false);


        if (PlayerPrefs.GetInt("Conto", 0) == 1 && !ended)
        {
            StartCoroutine(Delay(dialogue1, 0.2f));
        }

        if (PlayerPrefs.GetInt("Conto", 0) == 2 && !ended)
        {
            StartCoroutine(Delay(dialogue2, 0.2f));
        }

        if (PlayerPrefs.GetInt("Conto", 0) == 3 && !ended)
        {
            StartCoroutine(Delay(dialogue3, 0.2f));
        }

        if (ended)
            StartCoroutine(Delay(dialogue4, 0.2f));
    }

    void Start()
    {

    }

    void Update()
    {
        firstPerson.mouseSensitivity = PlayerPrefs.GetFloat("Sensibilidade", 3f);
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

                    else if (selection.GetComponent<DoorEnd>() != null && ended)
                    {
                        var doorEnd = selection.GetComponent<DoorEnd>();
                        doorEnd.EndGame();
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

    private IEnumerator Delay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}
