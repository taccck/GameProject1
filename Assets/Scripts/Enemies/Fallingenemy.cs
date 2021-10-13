using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Fallingenemy : MonoBehaviour
    {
        public Transform player;
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private float gravity = 1f;
        [SerializeField] private float angle = 45f;
        [SerializeField] private float lifeTime = 5f;

        [HideInInspector] private LayerMask collisionmask;
        private float spawnTime;

        private void FixedUpdate()
        {
            if (player != null)
                if (180f - Vector3.Angle(Vector3.up + transform.position, -(transform.position - player.position)) <
                    angle)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position,
                        -(transform.position - player.position).normalized,
                        Vector3.Distance(transform.position, player.position), collisionmask);
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                        body.gravityScale = gravity;
                }
            
            if (Time.time - spawnTime > lifeTime) Destroy(gameObject);
        }

        private void Awake()
        {
            collisionmask = LayerMask.GetMask("Floor", "Player");
            spawnTime = Time.time;
        }
    }
}