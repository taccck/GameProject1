using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private float damage;

        public float Getdamage()
        {
            return damage;
        }
    }
}