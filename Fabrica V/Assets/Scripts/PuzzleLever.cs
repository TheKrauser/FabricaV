using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLever : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    private SpriteRenderer render;
    [SerializeField] private int position;
    private bool on;
    private bool canSwitch;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        canSwitch = false;
        render.color = Color.red;
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
            render.color = Color.green;
            puzzleManager.test[position] = true;
            puzzleManager.CheckPuzzleLever();
        }
        else
        {
            on = false;
            render.color = Color.red;
            puzzleManager.test[position] = false;
            puzzleManager.CheckPuzzleLever();
        }
    }
}
