using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private LayerMask floorMaks;

    private Rigidbody2D body;
    private CapsuleCollider2D playerCollider;
    private int walkDirection = 0;

    private void OnMove(InputValue value)
    {
        walkDirection = (int) value.Get<float>();
        if (walkDirection != 0)
            transform.localScale = new Vector3(walkDirection * -1, 1, 1);
    }

    private void OnJump()
    {
        if (OnGround()) body.AddForce(Vector2.up * 500);
    }

    private void Walk() => body.velocity = new Vector2((Vector2.right * walkDirection * walkSpeed).x, body.velocity.y);

    private bool OnGround() => Physics2D.BoxCast((Vector2) transform.position + playerCollider.offset,
        playerCollider.size - new Vector2(.1f, 0), 0, Vector2.down,
        .1f, floorMaks);

    private void Update()
    {
        Walk();
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }
}