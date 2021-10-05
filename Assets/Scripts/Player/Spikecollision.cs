using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Spikecollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.CompareTag("Spike"))
            {

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Spike"))
            {

            }
        }
    }
}