using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Collisions : MonoBehaviour
    {
        [SerializeField] private Collider2D collider;
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private float invultime = 1f;

        [HideInInspector] private bool invul = false;
        [HideInInspector] private Coroutine invulroutine;

        private IEnumerator Invulperiod()
        {
            yield return new WaitForSeconds(invultime);
            invul = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Lava")
                Debug.Log("You ded");
            else if (collision.CompareTag("Danger"))
                if (!invul)
                {
                    invul = true;
                    invulroutine = StartCoroutine("Invulperiod");
                    inventory.Drop();
                }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Platform"))
            {
                collider.enabled = false;
                Debug.Log("enter");
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Platform"))
            {
                collider.enabled = true;
                Debug.Log("exit");
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Platform"))
            {
                Debug.Log("stay");
            }
        }
    }
}