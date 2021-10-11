using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Fallingenemy : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private float gravity = 1f;
        [SerializeField] private float angle = 45f;

        [HideInInspector] private LayerMask collisionmask;

        private void FixedUpdate()
        {
            if (180f - Vector3.Angle(Vector3.up + transform.position, -(transform.position - player.position)) < angle)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -(transform.position - player.position).normalized, Vector3.Distance(transform.position, player.position), collisionmask);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                    body.gravityScale = gravity;
            }
        }

        private void Awake()
        {
            collisionmask = LayerMask.GetMask("Floor", "Player");
        }
    }
}