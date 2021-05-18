using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLever : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    private SpriteRenderer render;
    private SpriteRenderer baseRender;
    private Animator anim;
    [SerializeField] private int position;
    private bool on;
    private bool canSwitch;
    public static bool puzzleSolved;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        baseRender = GetComponentsInChildren<SpriteRenderer>()[1];
        anim = GetComponent<Animator>();
        canSwitch = false;
        baseRender.color = Color.red;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canSwitch)
                Switch();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSwitch = false;
        }
    }

    public void Switch()
    {
        if (!on)
        {
            on = true;
            anim.SetBool("isActive", on);
            baseRender.color = Color.green;
            puzzleManager.attempt[position] = true;
            puzzleManager.CheckPuzzleLever();
        }
        else
        {
            on = false;
            anim.SetBool("isActive", on);
            baseRender.color = Color.red;
            puzzleManager.attempt[position] = false;
            puzzleManager.CheckPuzzleLever();
        }
    }
}
