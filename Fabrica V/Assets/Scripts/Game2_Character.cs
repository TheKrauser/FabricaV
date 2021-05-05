using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2_Character : MonoBehaviour
{
    public float speed;
    public float offset;
    public Transform movePoint;

    public LayerMask cantMoveLayers;

    private Animator anim;
    private int horAnim, verAnim;
    private bool moveUp, moveDown, moveLeft, moveRight;

    private bool canInteract;
    private DialogueSystem dial;

    private State state;

    public enum State
    {
        IDLE,
        DIALOGUE,
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        movePoint.parent = null;

        state = State.IDLE;
    }

    void Update()
    {
        switch(state)
        {
            case State.IDLE:
                Movement();
                Interact();
                break;

            case State.DIALOGUE:
                break;
        }
    }

    void Movement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.035f)
        {
            if (hor == 0 && ver == 0)
                ResetAnim();

            else if (Mathf.Abs(hor) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(hor * offset, 0, 0), 0.2f, cantMoveLayers))
                {
                    if (hor == 1)
                    {
                        ResetAnim();
                        anim.SetBool("moveRight", true);
                    }
                    else if (hor == -1)
                    {
                        ResetAnim();
                        anim.SetBool("moveLeft", true);
                    }

                    movePoint.position += new Vector3(hor * offset, 0, 0);
                }
            }

            else if (Mathf.Abs(ver) == 1f)
            {
                if (ver == 1)
                {
                    ResetAnim();
                    anim.SetBool("moveUp", true);
                }
                else if (ver == -1)
                {
                    ResetAnim();
                    anim.SetBool("moveDown", true);
                }

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, ver * offset, 0), 0.2f, cantMoveLayers))
                {
                    movePoint.position += new Vector3(0, ver * offset, 0);
                }
            }
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canInteract)
            {
                canInteract = false;
                state = State.DIALOGUE;
                dial.enabled = true;
                StartCoroutine(dial.DisplayText());
            }
        }
    }

    public void ChangeState(State stateS)
    {
        state = stateS;

        switch(stateS)
        {
            case State.IDLE:
                break;

            case State.DIALOGUE:
                break;
        }
    }

    void ResetAnim()
    {
        anim.SetBool("moveRight", false);
        anim.SetBool("moveLeft", false);
        anim.SetBool("moveUp", false);
        anim.SetBool("moveDown", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            canInteract = true;
            dial = collision.GetComponent<DialogueSystem>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            canInteract = false;
            dial = null;
        }
    }

    public void SetDialogue()
    {
        ChangeState(State.DIALOGUE);
    }
}
