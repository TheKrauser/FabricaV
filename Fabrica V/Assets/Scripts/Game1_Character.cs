using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using Photon.Pun;
using Photon.Realtime;

public class Game1_Character : MonoBehaviour
{

    PhotonView photonView;

    private Vector2 moveDir;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private BoxCollider2D box2D;
    public float speed;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    private bool isJumping;
    private bool facingRight;
    public float test;
    public float test2;

    private Transform visuals;

    public LayerMask groundMask;
    [SerializeField] private bool isGrounded;

    public Transform cmFar, cmClose;

    public PlayableDirector playable;

    [SerializeField] private float raycastLength;
    [SerializeField] private Transform g1, g2, g3;
    private float x;

    private bool inputJump = false;

    private State state;

    public enum State
    {
        IDLE,
        DIALOGUE,
        PAUSE,
    }

    private void Start()
    {
        anim = transform.Find("Visuals").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        isJumping = false;
        visuals = transform.Find("Visuals").GetComponent<Transform>();
        facingRight = true;

        PrefsManager.SetConto(1);

        AudioManager.Instance.PlaySoundtrack("Game1");
        photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        //Debug.Log(rb.velocity.y);
        if (photonView!= null && !photonView.IsMine)
        {
            return;
        }

        switch (state)
        {
            case State.IDLE:

                x = Input.GetAxisRaw("Horizontal");

                if (!inputJump && isGrounded)
                    inputJump = Input.GetKeyDown(KeyCode.Space);

                if (rb.velocity.y < 0)
                {
                    isGrounded = CheckGround();
                }

                anim.SetFloat("Speed", Mathf.Abs(x));


                if (x > 0)
                    facingRight = true;
                else if (x < 0)
                    facingRight = false;

                if (facingRight)
                    transform.localScale = new Vector3(1, 1, 1);
                else
                    transform.localScale = new Vector3(-1, 1, 1);

                moveDir = new Vector2(x, 0);

                break;

            case State.DIALOGUE:
                x = 0;

                anim.SetFloat("Speed", 0);
                break;

            case State.PAUSE:
                break;
        }

        /*Vector2 pos = transform.position;
        pos += moveDir * speed * Time.deltaTime;
        transform.position = pos;*/

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (rb.velocity.y > 0)
        {
            isGrounded = false;
            //anim.SetBool("isJumping", true);
        }


        anim.SetBool("isJumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.IDLE:

                if (Input.GetKey(KeyCode.A))
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                else
                {
                    if (Input.GetKey(KeyCode.D))
                        rb.velocity = new Vector2(+speed, rb.velocity.y);
                    else
                        rb.velocity = new Vector2(0, rb.velocity.y);
                }

                if (inputJump)
                {
                    Jump();
                }

                //Chegar no final mais rapidamente
                //if (Input.GetKeyDown(KeyCode.O))
                //{
                //    transform.position = new Vector2(-12.5f, 63f);
                //}

                break;

            case State.DIALOGUE:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }
    }

    void Jump()
    {
        AudioManager.Instance.PlaySoundEffect("Jump");
        rb.velocity = Vector2.up * jumpForce;
        inputJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            cmClose.gameObject.SetActive(false);
            cmFar.gameObject.SetActive(true);
        }

        if (collision.CompareTag("Scene"))
        {
            AudioManager.Instance.StopSoundtrack("Game1");
            LoadingScene.Instance.StartCoroutine(LoadingScene.Instance.FadeCutscene(2f, playable));
            ChangeState(State.DIALOGUE);
        }
    }

    private bool CheckGround()
    {
        bool ground;

        #region Um monte de Bosta
        //RaycastHit2D ray = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y - 1f), box2D.bounds.size - new Vector3(0.1f, 1f, 0f), 0f, Vector2.down, test, groundMask); ;
        //RaycastHit2D ray = Physics2D.Raycast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y - (box2D.bounds.extents.y / 2)), Vector2.down, box2D.bounds.extents.y + 0.15f, groundMask);
        //Debug.DrawRay(box2D.bounds.center - new Vector3(box2D.bounds.extents.x, box2D.bounds.extents.y + 1f), Vector2.right * (box2D.bounds.extents.x * 2f));

        /*RaycastHit2D ray1 = Physics2D.CircleCast(g1.position, raycastLength, Vector3.zero, groundMask);
        RaycastHit2D ray2 = Physics2D.CircleCast(g2.position, raycastLength, Vector3.zero, groundMask);
        RaycastHit2D ray3 = Physics2D.CircleCast(g3.position, raycastLength, Vector3.zero, groundMask);

        RaycastHit2D rayRay = Physics2D.Raycast(g1.position, Vector2.down, raycastLength, groundMask);
        Debug.DrawRay(g1.position, Vector2.down * raycastLength, Color.red);*/
        #endregion

        Collider2D[] ray = Physics2D.OverlapCircleAll(g1.position, raycastLength, groundMask);
        Collider2D[] ray2 = Physics2D.OverlapCircleAll(g3.position, raycastLength, groundMask);

        if (ray.Length > 0 || ray2.Length > 0)
        {
            ground = true;
        }
        else
        {
            ground = false;
        }

        return ground;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(g1.position, raycastLength);
        Gizmos.DrawWireSphere(g3.position, raycastLength);
    }

    public void ChangeState(State stateS)
    {
        state = stateS;

        switch (stateS)
        {
            case State.IDLE:
                break;

            case State.DIALOGUE:
                break;
        }
    }

    public void SetDialogue()
    {
        ChangeState(State.DIALOGUE);
    }

    public void Bounce()
    {
        AudioManager.Instance.PlaySoundEffect("Cloud");
        rb.velocity = Vector2.up * 15f;
    }
}
