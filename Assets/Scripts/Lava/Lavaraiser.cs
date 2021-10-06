using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Lavaraiser : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float heightcap;
        [SerializeField] private int maxrange = 10;
        [SerializeField] private float speedfactor = 1f;
        [SerializeField] private bool multicatchup = false;
        [SerializeField] private float speedupdateinterval = 1f;
        [SerializeField] private Transform player;

        [HideInInspector] private Vector3 target;
        [HideInInspector] private Coroutine updateroutine;
        [HideInInspector] private float speedamp;

        private IEnumerator Speedupdater()
        {
            while(true)
            {
                yield return new WaitForSeconds(speedupdateinterval);

                if (player.position.y - transform.position.y > maxrange)
                    if(multicatchup)
                        speedamp = ((player.position.y - transform.position.y) / maxrange) * speedfactor;
                    else
                        speedamp = speedfactor;
                else if(player.position.y - transform.position.y < maxrange)
                    speedamp = 1f;
            }
        }

        private void FixedUpdate()
        {
            if(transform.position.y < heightcap)
                transform.position = Vector3.MoveTowards(transform.position, target, speedamp * speed * Time.deltaTime);
        }

        private void Awake()
        {
            target = new Vector3(0, heightcap, 0);
            updateroutine = StartCoroutine("Speedupdater");
        }

        private void OnDisable()
        {
            StopCoroutine(updateroutine);
        }
    }
}