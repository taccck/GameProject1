using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Followplayer : MonoBehaviour
    {
        [HideInInspector] private Camera camera;

        [SerializeField] private Transform playerpos;

        private void FixedUpdate()
        {
            camera.transform.position = new Vector3(camera.transform.position.x, playerpos.position.y, camera.transform.position.z);
        }

        private void Awake()
        {
            camera = Camera.main;
        }
    }
}