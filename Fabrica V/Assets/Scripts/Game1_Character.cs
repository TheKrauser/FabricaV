using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Transform visuals;

    public LayerMask groundMask;
    private bool isGrounded;

    private void Start()
    {
        anim = transform.Find("Visuals").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        isJumping = false;
        visuals = transform.Find("Visuals").GetComponent<Transform>();
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

        isGrounded = CheckGround();

        anim.SetBool("isJumping", !isGrounded);
    }

    private void FixedUpdate()
    {
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

    }

    private bool CheckGround()
    {
        RaycastHit2D ray = Physics2D.Raycast(box2D.bounds.center, Vector2.down, box2D.bounds.extents.y + 0.1f, groundMask);
        Debug.DrawRay(box2D.bounds.center, Vector2.down * (box2D.bounds.extents.y + 0.1f));
        return ray.collider != null;
    }
}
