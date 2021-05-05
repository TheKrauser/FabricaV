using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G1_PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private BoxCollider2D box2D;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask layerGround;

    [Header("Movement")]
    [SerializeField] private float movementAcc = 50f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 10f;
    private float direction;
    private bool directionChange => (rb.velocity.x > 0f && direction < 0f) || (rb.velocity.x < 0f && direction > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private float extraJumps = 1f;
    [SerializeField] private float jumpsRemaining;
    [SerializeField] private bool isGround;
    private bool canJump => Input.GetKeyDown(KeyCode.Space) && (isGround || jumpsRemaining > 0f);

    [Header("Raycast Variables")]
    [SerializeField] private float groundRaycastLength;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        direction = GetInputs().x;

        if (canJump)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();

        if (isGround)
        {
            jumpsRemaining = extraJumps;
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            BetterJump();
        }
    }

    private Vector2 GetInputs()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        rb.AddForce(new Vector2(direction, 0f) * movementAcc);

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
        }
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(direction) < 0.4f || directionChange)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }
    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }

    private void Jump()
    {
        if (!isGround)
            jumpsRemaining--;

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void BetterJump()
    {
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0f && !Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void CheckGround()
    {
        isGround = Physics2D.Raycast(box2D.bounds.center, Vector2.down, groundRaycastLength, layerGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(box2D.bounds.center, box2D.bounds.center + Vector3.down * groundRaycastLength);
    }
}
