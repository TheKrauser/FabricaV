using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private int index;
    [HideInInspector] public bool triggered;
    public static bool puzzleSolved;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            AudioManager.Instance.PlaySoundEffect("Button");
            anim.SetBool("isPressed", triggered);
            //sprite.color = Color.green;
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
