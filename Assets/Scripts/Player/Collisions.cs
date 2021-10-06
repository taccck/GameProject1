using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Collisions : MonoBehaviour
    {
        [SerializeField] private PlayerInventory inventory;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Lava")
            {
                Debug.Log("You ded");
            }
            else if (collision.CompareTag("Danger"))
            {
                inventory.Drop();
            }
        }
    }
}