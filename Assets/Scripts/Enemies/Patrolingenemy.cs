using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Patrolingenemy : MonoBehaviour
    {
        [SerializeField] private int patrolwidth = 0;
        [SerializeField] private float speed = 0f;

        [HideInInspector] private int direction = -1;
        [HideInInspector] private float target;
        [HideInInspector] private Rigidbody2D rigidbody;

        private void FixedUpdate()
        {
            if (direction < 0)
                if (transform.position.x <= target)
                {
                    direction = 1;
                    target = transform.position.x + patrolwidth;
                }
                else
                    ;
            else
                if (transform.position.x >= target)
            {
                direction = -1;
                target = transform.position.x - patrolwidth;
            }


            rigidbody.velocity = new Vector2((Vector2.right * direction * speed).x, 0);
            transform.localScale = new Vector3(direction * -1, 1, 1);
        }

        private void Awake()
        {
            target = transform.position.x - patrolwidth;
            rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}