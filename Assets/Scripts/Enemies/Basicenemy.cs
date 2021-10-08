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
        [HideInInspector] private Vector3 orgpos;

        private void FixedUpdate()
        {
            if(!direction)
                if (transform.position.x <= orgpos.x - patrolwidth)
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(orgpos.x - patrolwidth, 0, 0), speed * Time.deltaTime);
            else
                if (transform.position.x >= orgpos.x)
                    transform.position = Vector3.MoveTowards(transform.position, orgpos, speed * Time.deltaTime);
        }

        private void Awake()
        {
            orgpos = transform.position;
        }
    }
}