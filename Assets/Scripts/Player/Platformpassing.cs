using System.Collections;
using UnityEngine;

namespace FG
{
    public class Platformpassing : MonoBehaviour
    {
        [SerializeField] private Collider2D collider;

        [HideInInspector] private Rigidbody2D body;

        public void Fall()
        {
            collider.isTrigger = true;

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                collider.isTrigger = false;

                body.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Floor") && collider.isTrigger)
                body.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }
    }
}
