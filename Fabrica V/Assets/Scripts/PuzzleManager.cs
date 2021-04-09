using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] buttons;
    public List<GameObject> buttonsList = new List<GameObject>();
    public List<int> buttonsNumber = new List<int>();
    public int[] password = { 4, 2, 5, 9, 1, 3, 7, 8, 6 };
    public bool rightNumber;
    public bool WrongNumber;
    public int buttonsPressed = 0;
    public int guessedNumbers = 0;

    private void Update()
    {
        if (guessedNumbers == 9)
        {
            guessedNumbers = 0;
            rightNumber = true;
            Debug.Log("Password Correct!");
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
    }

    private void ResetPuzzle()
    {
        for (int i=0; i<buttons.Length; i++)
        {
            var buttonScript = buttons[i].GetComponent<PuzzleButton>();
            var sRender = buttons[i].GetComponent<SpriteRenderer>();
            sRender.color = Color.white;
            buttonScript.triggered = false;
            buttonsNumber.Clear();
        }
    }
}

