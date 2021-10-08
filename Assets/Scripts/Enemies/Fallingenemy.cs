using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Fallingenemy : MonoBehaviour
    {
        [SerializeField] private Vector3 player;
        [SerializeField] private Rigidbody2D body;

        private void FixedUpdate()
        {
            if (transform.position == player)
                ;

        }
    }
}