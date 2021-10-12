using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Clickzone : MonoBehaviour
    {
        [Tooltip("% / operation")]
        [SerializeField] private float percentage;
        [Tooltip("Padding for red bar, in width/2")]
        [SerializeField] private int padding = 1;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform bar;
        [HideInInspector] private Transform red;
        [HideInInspector] private bool goright = true;

        public void Addprogress()
        {
            if (goright)
            {
                progress.localPosition += new Vector3(percentage, red.localPosition.y);

                if (progress.localPosition.x >= bar.localScale.x / 2f)
                    goright = false;
            }
            else if (!goright)
            {
                progress.localPosition -= new Vector3(percentage, red.localPosition.y);

                if (progress.localPosition.x <= -bar.localScale.x / 2f)
                    goright = true;
            }
        }

        public bool Isinzone()
        {
            RaycastHit2D hit = Physics2D.Raycast(progress.position, Vector2.up, 1f);
            if(hit.collider != null && hit.collider.CompareTag("Progbar"))
                return true;
            return false;
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

            percentage *= bar.localScale.x / 100f;

            Redloc();

            goright = Random.Range(0, 1) > 0.5f;
        }
    }
}