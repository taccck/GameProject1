using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Progresscircle : MonoBehaviour
    {
        [Tooltip("% / operation")]
        [SerializeField] private float percentage;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform circle;
        [HideInInspector] private bool swapped = false;

        public bool Isfilled()
        {
            if (progress.localScale.x >= circle.localScale.x * 2)
                return true;
            return false;
        }

        public void Addprogress()
        {
            progress.localScale += new Vector3(percentage, percentage);

            if (!swapped && progress.localScale.x > circle.localScale.x)
            {
                circle.GetComponent<SpriteRenderer>().sortingOrder = 2;
                swapped = true;
            }
        }

        private void Awake()
        {
            progress = transform.Find("Progress");
            circle = transform.Find("Circle");

            progress.localScale = new Vector3(0, 0);

            percentage *= circle.localScale.x / 100;
        }
    }
}