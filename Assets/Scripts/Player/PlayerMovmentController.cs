using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FG
{
    public class PlayerMovmentController : MonoBehaviour
    {
        [NonSerialized] private bool walking;
        [NonSerialized] private bool jumping;
        [NonSerialized] private bool bonking;

        [SerializeField, Tooltip("Meters / second"), Header("Walking")]
        private float walkSpeed = 5f;

        [SerializeField, Tooltip("Seconds"), Header("Jumping")]
        private float maxJumpTime = .5f;

        [SerializeField, Tooltip("Meters / second")]
        private float jumpSpeed = 7f;

        private LayerMask floorMaks;
        private Rigidbody2D body;
        private CapsuleCollider2D playerCollider;
        private PlayerAnimationController animController;
        private int walkDirection = 0;
        private float currJumpTime = 0;
        private bool onGround;

        private const float SMALL_OFFSET = .1f;

        [SerializeField] private Platformpassing platformpassing;

        [HideInInspector] private float rayrange = 2f;

        private void OnMove(InputValue value)
        {
            walkDirection = (int) value.Get<float>();
            walking = walkDirection != 0;
            if (walking && !bonking)
                transform.localScale = new Vector3(walkDirection * -1, 1, 1);
        }

        private void OnJump(InputValue value) => jumping = value.isPressed && onGround;

        private void OnFallthrough(InputValue input)
        {
            if (input.isPressed && onGround)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, -0.5f, 0f), -transform.up, rayrange);
                if (hit.collider != null && hit.collider.CompareTag("Platform"))
                    platformpassing.Fall();
            }
        }
        
        private void OnDash(InputValue value)
        {
            
        }

        private void FixedUpdate()
        {
            OnGround();
            Walk();
            Jump();
            Animate();
        }

        private void OnGround()
        {
            onGround = Physics2D.BoxCast((Vector2) transform.position + playerCollider.offset,
                playerCollider.size - new Vector2(SMALL_OFFSET, SMALL_OFFSET), 0, Vector2.down,
                SMALL_OFFSET, floorMaks);

            if (onGround) bonking = false;
        }

        private void Walk()
        {
            if (!bonking)
                body.velocity = new Vector2((Vector2.right * walkDirection * walkSpeed).x, body.velocity.y);
        }

        private void Jump()
        {
            body.gravityScale = 1;
            currJumpTime += Time.deltaTime;
            if (!jumping || currJumpTime >= maxJumpTime)
            {
                currJumpTime = 0;
                jumping = false;
                return;
            }

            RaycastHit2D roofCheck = Physics2D.Raycast(
                (Vector2) transform.position + new Vector2(0, playerCollider.size.y), Vector2.up,
                SMALL_OFFSET, floorMaks);
            if (roofCheck)
            {
                bonking = true;
                jumping = false;
            }

            body.gravityScale = 0;
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        private void Animate()
        {
            if (bonking)
                animController.ChangeAnimationState(animController.Bonk);
            else if (jumping)
                animController.ChangeAnimationState(animController.Jump);
            else if (onGround)
                animController.ChangeAnimationState(animController.Idle);
        }

        private void Awake()
        {
            floorMaks = LayerMask.GetMask("Floor");

            body = GetComponent<Rigidbody2D>();
            playerCollider = GetComponent<CapsuleCollider2D>();
            animController = GetComponentInChildren<PlayerAnimationController>();
        }
    }
}