using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3_Character : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private float x, y;
    private Vector2 moveDir;
    private bool isWalking;
    [SerializeField] private float speed;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("x", x);
        anim.SetFloat("y", y);

        moveDir = new Vector2(x, y);

        if (x != 0 || y != 0)
        {
            isWalking = true;
            anim.SetBool("isWalking", isWalking);
        }
        else
        {
            isWalking = false;
            anim.SetBool("isWalking", isWalking);
        }
    }

    void FixedUpdate()
    {
        if (x != 0 || y != 0)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.velocity = moveDir * speed * Time.fixedDeltaTime;
    }
}
