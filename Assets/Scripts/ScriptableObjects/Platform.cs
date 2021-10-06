using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Collider2D collider;

        private void Update()
        {
            if (collider.enabled && player.position.y + 0.5 < transform.position.y)
                collider.enabled = false;

            else if (!collider.enabled && player.position.y - 0.5 > transform.position.y)
                collider.enabled = true;


        }
    }
}