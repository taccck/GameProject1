using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FG
{
    public class PlayerMovmentController : MonoBehaviour
    {
        [NonSerialized] public bool bonking;
        
        [SerializeField, Tooltip("m/s"), Header("Walking")]
        private float walkSpeed = 5f;

        [SerializeField, Tooltip("Seconds"), Header("Jumping")]
        private float maxJumpTime = .5f;

        [SerializeField, Tooltip("m/s")] private float jumpSpeed = 7f;

        [SerializeField, Tooltip("kg * m/s"), Header("Dashing")]
        private float dashForce = 8f;

        [SerializeField, Tooltip("m/s"), Header("Knockback")]
        private float knockbackSpeed = 6f;

        [SerializeField] private float lavaKnockbackSpeed = 30f;

        private LayerMask floorMaks;
        private Rigidbody2D body;
        private CapsuleCollider2D playerCollider;
        private PlayerAnimationController animController;
        private int walkDirection;
        private float currJumpTime;
        private bool onGround;
        private bool walking;
        private bool jumping;
        private bool dashing;

        private const float SMALL_OFFSET = .1f;

        [SerializeField] private Platformpassing platformpassing;

        [HideInInspector] private float rayrange = 2f;
        [HideInInspector] private bool paused = false;

        public void Knockback(bool right, bool up)
        {
            bonking = true;
            Vector2 knockbackDir = Vector2.zero;
            knockbackDir.y = up ? 1f : -1f;
            knockbackDir.x = right ? 1f : -1f;
            knockbackDir = knockbackDir.normalized * knockbackSpeed;
            body.velocity = knockbackDir;
            transform.position += new Vector3(0, .2f, 0); //move so not on ground when knockbacking

            AudioManager.Curr.Play("Bonk");
        }

        public void LavaKnockback()
        {
            Vector2 knockbackDir = Vector2.up * lavaKnockbackSpeed;
            body.velocity = knockbackDir;
        }

        private void OnMove(InputValue value)
        {
            walkDirection = (int) value.Get<float>();
            walking = walkDirection != 0;
        }

        private void OnJump(InputValue value)
        {
            jumping = value.isPressed && onGround;
            if (jumping) AudioManager.Curr.Play("Jump");
        }

        private void OnFallthrough(InputValue input)
        {
            if (!input.isPressed || !onGround) return;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, rayrange, floorMaks);
            if (hit.collider != null && hit.collider.CompareTag("Platform"))
                platformpassing.Fall();
        }

        private void OnDash(InputValue value)
        {
            if (dashing || onGround || bonking) return;

            jumping = false;
            dashing = true;
            body.AddForce(Vector2.right * transform.localScale.x * dashForce * -1f, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
                OnGround();
                Walk();
                Jump();
                Dash();
                Animate();
        }

        private void OnGround()
        {
            onGround = Physics2D.BoxCast((Vector2) transform.position + playerCollider.offset,
                playerCollider.size - new Vector2(SMALL_OFFSET, SMALL_OFFSET), 0, Vector2.down,
                SMALL_OFFSET, floorMaks);

            if (!onGround) return;

            bonking = false;
            dashing = false;
        }

        private void Walk()
        {
            if (!bonking && !dashing)
            {
                body.velocity = new Vector2((Vector2.right * walkDirection * walkSpeed).x, body.velocity.y);
                if (walking)
                    transform.localScale = new Vector3(walkDirection * -1, 1, 1);
            }
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
                if (!roofCheck.transform.CompareTag("Platform"))
                {
                    jumping = false;
                }
            }

            body.gravityScale = 0;
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        private void Dash()
        {
            if (!dashing) return;
            RaycastHit2D sideCheck = Physics2D.BoxCast((Vector2) transform.position + playerCollider.offset,
                playerCollider.size - new Vector2(SMALL_OFFSET, SMALL_OFFSET), 0,
                Vector2.right * transform.localScale.x * -1f,
                SMALL_OFFSET, floorMaks);

            if (!sideCheck) return;
            dashing = false;
            Knockback(Mathf.Approximately(transform.localScale.x, 1f), true);
        }

        private void Animate()
        {
            if (bonking)
                animController.ChangeAnimationState(animController.Bonk);
            else if (dashing)
                animController.ChangeAnimationState(animController.Dash);
            else if (jumping)
                animController.ChangeAnimationState(animController.Jump);
            else if (walking && onGround)
                animController.ChangeAnimationState(animController.Walk);
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