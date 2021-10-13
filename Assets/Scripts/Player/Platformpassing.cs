using System.Collections;
using UnityEngine;

namespace FG
{
    public class Platformpassing : MonoBehaviour
    {
        [SerializeField] private Collider2D collider;
        [SerializeField] private bool freezex = false;

        [HideInInspector] private Rigidbody2D body;

        public void Fall()
        {
            collider.isTrigger = true;

            if(freezex)
                body.constraints = RigidbodyConstraints2D.FreezePositionX;

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                collider.isTrigger = false;

                if(freezex)
                    body.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }
    }
}
