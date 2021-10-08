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
        [NonSerialized] private bool dashing;

        [SerializeField, Tooltip("Meters / second"), Header("Walking")]
        private float walkSpeed = 5f;

        [SerializeField, Tooltip("Seconds"), Header("Jumping")]
        private float maxJumpTime = .5f;

        [SerializeField, Tooltip("Meters / second")]
        private float jumpSpeed = 7f;

        [SerializeField, Tooltip("Newton"), Header("Dashing")]
        private float dashForce = .5f;

        [SerializeField, Tooltip("Meters / second"), Header("Knockback")]
        private float knockbackSpeed = 1f;

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
        [HideInInspector] private bool paused = false;

        public void Knockback(bool right, bool up)
        {
            bonking = true;
            Vector2 knockbackDir = Vector2.zero;
            knockbackDir.y = up ? 1f : -1f;
            knockbackDir.x = right ? 1f : -1f;
            knockbackDir.Normalize();
            print(knockbackDir);

            body.AddForce(knockbackDir * knockbackSpeed);
        }

        private void OnMove(InputValue value)
        {
            walkDirection = (int) value.Get<float>();
            walking = walkDirection != 0;
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

        private bool Togglepause()
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                return (false);
            }
            else
            {
                Time.timeScale = 0f;
                return (true);
            }
        }

        private void OnMenu(InputValue input)
        {
            paused = Togglepause();
        }

        private void OnDash(InputValue value)
        {
            if (!dashing && !onGround)
            {
                dashing = true;
                body.AddForce(Vector2.right * transform.localScale.x * dashForce * -1f);
            }
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

            if (onGround)
            {
                bonking = false;
                dashing = false;
            }
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
            if (dashing)
            {
                RaycastHit2D sideCheck = Physics2D.BoxCast((Vector2) transform.position + playerCollider.offset,
                    playerCollider.size - new Vector2(SMALL_OFFSET, SMALL_OFFSET), 0,
                    Vector2.right * transform.localScale.x * -1f,
                    SMALL_OFFSET, floorMaks);
                if (sideCheck)
                {
                    dashing = false;
                    Knockback(Mathf.Approximately(transform.localScale.x, 1f), true);
                }
            }
        }

        private void Animate()
        {
            if (bonking)
                animController.ChangeAnimationState(animController.Bonk);
            else if (dashing)
                animController.ChangeAnimationState(animController.Dash);
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

        void OnGUI()
        {
            if (paused)
            {
                GUILayout.Label("Game is paused! Hit ESC to unpause");
                if (GUILayout.Button("Click me to quit"))
                    Application.Quit();
            }
        }
    }
}