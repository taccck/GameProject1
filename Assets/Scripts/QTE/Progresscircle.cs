using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Progresscircle : MonoBehaviour
    {
        [Tooltip("% / interval")]
        [SerializeField] private float percentage;
        [SerializeField] private float interval = 1f;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform circle;
        [HideInInspector] private bool swapped = false;
        [HideInInspector] private bool started = false;

        public bool Isfilled()
        {
            if (progress.localScale.x / circle.localScale.x == 1)
                return true;
            return false;
        }

        public bool Startbar()
        {
            if (!started)
            {
                started = true;
                StartCoroutine("Addprogress");
                return true;
            }
            return false;
        }

        private IEnumerator Addprogress()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                progress.localScale += new Vector3(percentage, percentage);

                if (!swapped && progress.localScale.x > circle.localScale.x)
                {
                    circle.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    swapped = true;
                }
                
                if (Isfilled())
                    break;
            }
        }

        private void Awake()
        {
            progress = transform.Find("Progress");
            circle = transform.Find("Circle");

            progress.localScale = new Vector3(0, 0);

            percentage *= circle.localScale.x / 100f;
        }
    }
}