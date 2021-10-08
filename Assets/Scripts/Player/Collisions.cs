using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else if (collision.CompareTag("Danger"))
                if (!invul)
                {
                    invul = true;
                    invulroutine = StartCoroutine("Invulperiod");
                    inventory.Drop();
                }
        }
    }
}