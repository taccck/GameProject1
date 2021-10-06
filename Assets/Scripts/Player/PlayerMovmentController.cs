using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovmentController : MonoBehaviour
{
    [NonSerialized] public bool Walking;
    [NonSerialized] public bool Jumping;

    [SerializeField, Tooltip("Meters / second"), Header("Walking")]
    private float walkSpeed = 5f;

    [SerializeField, Tooltip("Seconds"), Header("Jumping")]
    private float maxJumpTime = .5f;

    [SerializeField, Tooltip("Meters / second")]
    private float jumpSpeed = 7f;

    [SerializeField] private LayerMask floorMaks;

    private Rigidbody2D body;
    private CapsuleCollider2D playerCollider;
    private int walkDirection = 0;
    private float currJumpTime = 0;

    private const float SMALL_OFFSET = .1f;

    private void OnMove(InputValue value)
    {
        walkDirection = (int) value.Get<float>();
        Walking = walkDirection != 0;
        if (Walking)
            transform.localScale = new Vector3(walkDirection * -1, 1, 1);
    }

    private void OnJump(InputValue value) => Jumping = value.isPressed && OnGround();


    private bool OnGround() => Physics2D.BoxCast((Vector2) transform.position + playerCollider.offset,
        playerCollider.size - new Vector2(SMALL_OFFSET, 0), 0, Vector2.down,
        SMALL_OFFSET, floorMaks);

    private void FixedUpdate()
    {
        Walk();
        Jump();
    }

    private void Walk() => body.velocity = new Vector2((Vector2.right * walkDirection * walkSpeed).x, body.velocity.y);

    private void Jump()
    {
        body.gravityScale = 1;
        currJumpTime += Time.deltaTime;
        if (!Jumping || currJumpTime >= maxJumpTime)
        {
            currJumpTime = 0;
            Jumping = false;
            return;
        }

        body.gravityScale = 0;
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }
}