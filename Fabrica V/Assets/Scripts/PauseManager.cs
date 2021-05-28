using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [HideInInspector] public bool canPause = true;
    [HideInInspector] public bool isPaused = false;

    [SerializeField] private Transform pause;
    public static PauseManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canPause)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pause.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            Time.timeScale = 0f;
            pause.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        isPaused = !isPaused;
    }

    public void ResetScene()
    {
        PauseGame();
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    public void MainMenu()
    {
        PauseGame();
        SceneManager.LoadScene("Menu");
    }
}
