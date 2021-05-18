using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle dos Botões")]
    public GameObject[] buttons;
    //public List<GameObject> buttonsList = new List<GameObject>();
    public List<int> buttonsNumber = new List<int>();
    public int[] password = { 4, 2, 5, 9, 1, 3, 7, 8, 6 };
    private bool rightNumber;
    private bool WrongNumber;
    [HideInInspector] public int buttonsPressed = 0;
    [HideInInspector] public int guessedNumbers = 0;

    [Header("Puzzle das Alavancas")]
    public bool[] levers = { false, true, true, true };
    public List<bool> attempts = new List<bool>(4);
    public bool[] attempt = { false, false, false, false };

    [SerializeField] private Transform puzzleDoor, puzzleCollider;
    [SerializeField] private Game2_Character charrr;

    private bool oneTimeActivatePuzzleComplete = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PuzzleButton.puzzleSolved = true;
            PuzzleLever.puzzleSolved = true;
        }

        if (PuzzleLever.puzzleSolved && PuzzleButton.puzzleSolved)
        {
            if (!oneTimeActivatePuzzleComplete)
            {
                StartCoroutine(charrr.TimerChangeState(3f));
                puzzleCollider.gameObject.SetActive(false);
                puzzleDoor.gameObject.SetActive(true);
                oneTimeActivatePuzzleComplete = true;
            }
        }
    }

    public void CheckPuzzle()
    {
        for (int i = 0; i < password.Length; i++)
        {
            if (password[i] == buttonsNumber[i])
            {
                guessedNumbers++;
            }
        }

        if (guessedNumbers != 9)
        {
            Debug.Log("Wrong Code");
            buttonsPressed = 0;
            guessedNumbers = 0;
            ResetPuzzle();
        }

        if (guessedNumbers == 9)
        {
            PuzzleButton.puzzleSolved = true;
            Debug.Log("Password Correct!");
            guessedNumbers = 0;
            rightNumber = true;
        }
    }

    private void ResetPuzzle()
    {
        for (int i=0; i<buttons.Length; i++)
        {
            var buttonScript = buttons[i].GetComponent<PuzzleButton>();
            var sRender = buttons[i].GetComponent<SpriteRenderer>();
            var anim = buttons[i].GetComponent<Animator>();
            //sRender.color = Color.white;
            buttonScript.triggered = false;
            anim.SetBool("isPressed", buttonScript.triggered);
            buttonsNumber.Clear();
        }
    }

    public void CheckPuzzleLever()
    {
        int rightPassword = 0;

        for (int i = 0; i < levers.Length; i++)
        {
            if (levers[i] == attempt[i])
            {
                rightPassword++;
            }
        }

        if (rightPassword == 4)
        {
            Debug.Log("Correct");
            PuzzleLever.puzzleSolved = true;
        }
        else
        {
            Debug.Log("Incorrect");
        }
        /*
        if (levers[0] == test[0])
            if (levers[1] == test[1])
                if (levers[2] == test[2])
                    if (levers[3] == test[3])
                        Debug.Log("Correct");
                    else
                        Debug.Log("Incorrect");
                else
                    Debug.Log("Incorrect");
            else
                Debug.Log("Incorrect");
        else
            Debug.Log("Incorrect");*/
    }
}

