using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Stayinzone : QTE
    {
        [Tooltip("% lost / operation")]
        [SerializeField] private float percentageloss = 0;
        [Tooltip("Gap between the bars")]
        [SerializeField] private float gap = 0.5f;

        [HideInInspector] private Transform red2;

        public override bool Isfilled()
        {
            if (Isinzone())
                return true;
            return false;
        }

        public override bool Startbar()
        {
            StartCoroutine(Addprogress());
            return base.Startbar();
        }

        public override void Interact()
        {
            if (!cd && progress.localPosition.x < bar.localScale.x / 2f)
            {
                progress.localPosition += new Vector3(percentage, 0);

                StartCoroutine(Cooldown());
            }
        }

        protected override IEnumerator Addprogress()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                if(progress.localPosition.x != -bar.localScale.x / 2f)
                    progress.localPosition -= new Vector3(percentage, 0);
            }
        }

        protected override void Redloc()
        {
            float width = red.localScale.x / 2f;
            float rand = Random.Range(padding * width + gap + width * 2 - bar.localScale.x / 2f, bar.localScale.x / 2f - width * 2 - gap - padding * width);
            red.localPosition = new Vector3(rand, red.localPosition.y);
            red2.localPosition = new Vector3(rand + gap, red.localPosition.y);
        }

        private bool Isinzone()
        {
            if (progress.localPosition.x > red.localPosition.x && progress.localPosition.x < red2.localPosition.x)
                return true;
            return false;
        }

        private void Awake()
        {
            progress = transform.Find("Progress");
            bar = transform.Find("Bar");
            red = transform.Find("Red");
            red2 = transform.Find("Red2");

            percentage *= bar.localScale.x / 100f;
            percentageloss *= bar.localScale.x / 100f;

            progress.localPosition = new Vector3(-bar.localScale.x / 2f, 0);

            Redloc();
        }
    }
}