using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Slippery : MonoBehaviour
    {
        [SerializeField] private float forcefactor = 1f;

        [HideInInspector] private float dir = 0f;
        [HideInInspector] private Rigidbody2D playerbod;
        [HideInInspector] private Vector2 force = new Vector2(100f, 0);

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                dir = collision.collider.transform.localScale.x * -1;

                playerbod.AddForce(force * dir * forcefactor);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                dir = collision.collider.transform.localScale.x * -1;

                playerbod.AddForce(force * dir * forcefactor);
            }
        }

        private void Awake()
        {
            playerbod = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        }
    }
}