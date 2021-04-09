using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private int index;
    [HideInInspector] public bool triggered;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            sprite.color = Color.green;
            puzzleManager.buttonsNumber.Add(index);
            Debug.Log(puzzleManager.buttonsNumber[puzzleManager.buttonsPressed]);
            puzzleManager.buttonsPressed++;

            if (puzzleManager.buttonsPressed == 9)
            {
                puzzleManager.buttonsPressed = 0;
                puzzleManager.CheckPuzzle();
            }
        }
    }
}
