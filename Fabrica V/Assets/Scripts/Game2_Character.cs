using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Game2_Character : MonoBehaviour
{
    public float speed;
    public float offset;
    public Transform movePoint;
    public Transform oldMovePointPosition;

    public LayerMask cantMoveLayers;

    private Animator anim;
    private int horAnim, verAnim;
    private bool moveUp, moveDown, moveLeft, moveRight;

    private bool canInteract;
    private DialogueSystem dial;

    private State state;
    private bool notWalking;

    public enum State
    {
        IDLE,
        DIALOGUE_IDLE,
        DIALOGUE_WALK,
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        movePoint.parent = null;
        oldMovePointPosition.parent = null;

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

            case State.DIALOGUE_IDLE:
                ResetAnim();
                break;

            case State.DIALOGUE_WALK:
                ResetAnim();
                transform.position = oldMovePointPosition.position;
                movePoint.position = oldMovePointPosition.position;
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

                    oldMovePointPosition.position = movePoint.position;
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
                    oldMovePointPosition.position = movePoint.position;
                    movePoint.position += new Vector3(0, ver * offset, 0);
                }
            }
        }
        else
            notWalking = false;
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canInteract && notWalking)
            {
                canInteract = false;
                state = State.DIALOGUE_IDLE;
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

            case State.DIALOGUE_IDLE:
                break;
        }
    }

    public IEnumerator TimerChangeState(float timer)
    {
        state = State.DIALOGUE_IDLE;

        CameraShake.Instance.ShakeCamera(0.5f, timer);

        yield return new WaitForSecondsRealtime(timer);

        state = State.IDLE;
    }

    void ResetAnim()
    {
        notWalking = true;
        anim.SetBool("moveRight", false);
        anim.SetBool("moveLeft", false);
        anim.SetBool("moveUp", false);
        anim.SetBool("moveDown", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            dial = collision.GetComponent<DialogueSystem>();

            if (!dial.activateOnTriggerEnter)
            canInteract = true;
        }

        if (collision.CompareTag("G2_End"))
        {
            LoadingScene.Instance.LoadScene("CasaConfig");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            dial = null;
            canInteract = false;
        }
    }

    public void SetDialogue()
    {
        ChangeState(State.DIALOGUE_IDLE);
    }
}
