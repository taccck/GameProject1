using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Collider2D collider;
        [SerializeField] private float passwindow = 0.5f;

        [HideInInspector] private Coroutine passroutine;
        [HideInInspector] private bool passing;

        private void Update()
        {
            if (!passing)
            {
                if (collider.enabled && player.position.y + 0.5 < transform.position.y)
                    collider.enabled = false;

                else if (!collider.enabled && player.position.y - 0.5 > transform.position.y)
                    collider.enabled = true;
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(passwindow);
            passing = false;
        }

        public void Fall()
        {
            passing = true;
            collider.enabled = false;
            passroutine = StartCoroutine("Timer");
        }
    }
}