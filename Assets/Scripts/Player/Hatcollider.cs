using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Hatcollider : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        [HideInInspector] private Platformpassing ghost;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
                ghost.Fall();
        }

        private void Awake()
        {
            ghost = player.GetComponent<Platformpassing>();
        }
    }
}