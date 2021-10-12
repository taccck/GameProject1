using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Stayinzone : MonoBehaviour
    {
        [Tooltip("% / operation")]
        [SerializeField] private float percentage;
        [Tooltip("% lost / operation")]
        [SerializeField] private float percentageloss = 0;
        [Tooltip("Number of operations required")]
        [SerializeField] private int operations = 1;
        [Tooltip("Padding for red bar, in width/2")]
        [SerializeField] private int padding = 1;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform bar;
        [HideInInspector] private Transform red;
        [HideInInspector] private Transform red2;
        [HideInInspector] private int opsdone = 0;
        [HideInInspector] private bool inzone = false;

        public bool Isfilled()
        {
            if (opsdone == operations)
                return true;
            return false;
        }

        public void Addprogress()
        {
            if (progress.localScale.x < bar.localScale.x)
            {
                progress.localScale += new Vector3(percentage, 0);
                progress.localPosition = new Vector3((-bar.localScale.x / 2f) + progress.localScale.x / 2f, progress.localPosition.y);
            }
        }

        public bool Isinzone()
        {
            return true;
        }

        private void Redloc()
        {
            float width = red.localScale.x / 2f;
            float rand = Random.Range(padding * width - bar.localScale.x / 2f, bar.localScale.x / 2f - padding * width);
            red.localPosition = new Vector3(rand, red.localPosition.y);
        }

        private void Awake()
        {
            progress = transform.Find("Progress");
            bar = transform.Find("Bar");
            red = transform.Find("Red");
            red2 = transform.Find("Red2");

            progress.localScale = new Vector3(0, progress.localScale.y);

            percentage *= bar.localScale.x / 100f;
            percentageloss *= bar.localScale.x / 100f;

            Redloc();
        }
    }
}