using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Basicenemy : MonoBehaviour
    {
        [SerializeField] private int patrolwidth = 0;
        [SerializeField] private float speed = 0f;

        [HideInInspector] private bool direction = false;
        [HideInInspector] private Vector3 target;

        private void FixedUpdate()
        {
            if(!direction)
                if (transform.position.x >= target.x - patrolwidth)
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x - patrolwidth, 0, 0), speed * Time.deltaTime);
            else
                if (transform.position.x <= target.x)
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        private void Awake()
        {
            target = transform.position;
        }
    }
}