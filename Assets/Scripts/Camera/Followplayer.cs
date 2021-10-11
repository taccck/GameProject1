using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Followplayer : MonoBehaviour
    {
        [HideInInspector] private Camera camera;

        [SerializeField, Range(0, .99f)] private float smoothness = .5f;
        [SerializeField] private Transform playerpos;

        private float reverseSmooth;

        private void FixedUpdate()
        {
            float currY = Mathf.Lerp(camera.transform.position.y, playerpos.position.y, reverseSmooth);

            camera.transform.position = new Vector3(camera.transform.position.x, currY, camera.transform.position.z);
        }

        private void Awake()
        {
            reverseSmooth = 1 - smoothness;
            camera = Camera.main;
        }
    }
}