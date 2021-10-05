using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Lavaraiser : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float heightcap;

        [HideInInspector] private Vector3 target;

        private void FixedUpdate()
        {
            if(transform.position.y < heightcap)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }

        private void Awake()
        {
            target = new Vector3(0, heightcap, 0);
        }
    }
}