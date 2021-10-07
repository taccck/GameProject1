using System.Collections;
using UnityEngine;

namespace FG
{
    public class Platformpassing : MonoBehaviour
    {
        [SerializeField] private Collider2D collider;

        public void Fall()
        {
            collider.isTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
                collider.isTrigger = false;
        }
    }
}
