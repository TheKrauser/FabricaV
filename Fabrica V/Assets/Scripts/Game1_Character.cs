using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
public class Game1_Character : MonoBehaviour
{
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D box2D;
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
    private bool isGrounded;

    public Transform cmFar, cmClose;

    public PlayableDirector playable;

    private void Start()
    {
        anim = transform.Find("Visuals").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        isJumping = false;
        visuals = transform.Find("Visuals").GetComponent<Transform>();
        facingRight = true;
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(x, 0);

        /*Vector2 pos = transform.position;
        pos += moveDir * speed * Time.deltaTime;
        transform.position = pos;*/

        anim.SetFloat("Speed", Mathf.Abs(x));

        if (x > 0)
            facingRight = true;
        else if (x < 0)
            facingRight = false;

        if (facingRight)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

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

        if (Input.GetKeyDown(KeyCode.P))
            playable.Play();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
            isGrounded = CheckGround();

        if (Input.GetKey(KeyCode.A))
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
        {
            if (Input.GetKey(KeyCode.D))
                rb.velocity = new Vector2(+speed, rb.velocity.y);
            else
                rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
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
            playable.Play();
    }

    private bool CheckGround()
    {
        //RaycastHit2D ray = Physics2D.BoxCast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y - 1f), box2D.bounds.size - new Vector3(0.1f, 1f, 0f), 0f, Vector2.down, test, groundMask); ;
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(box2D.bounds.center.x, box2D.bounds.center.y - (box2D.bounds.extents.y / 2)), Vector2.down, box2D.bounds.extents.y + 0.15f, groundMask);
        //Debug.DrawRay(box2D.bounds.center - new Vector3(box2D.bounds.extents.x, box2D.bounds.extents.y + 1f), Vector2.right * (box2D.bounds.extents.x * 2f));
        return ray.collider != null;
    }
}
