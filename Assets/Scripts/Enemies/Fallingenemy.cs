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

        [HideInInspector] private LayerMask playermask;

        private void FixedUpdate()
        {
            if (Vector3.Angle(-transform.up, player.position) < angle)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position, Vector3.Distance(transform.position, player.position), playermask);
                if(hit.collider != null)
                    Debug.Log(hit.collider.tag);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                    body.gravityScale = gravity;
            }
        }

        private void Awake()
        {
            playermask = LayerMask.GetMask("Player");
        }
    }
}