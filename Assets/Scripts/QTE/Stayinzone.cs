using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Stayinzone : MonoBehaviour
    {
        [Tooltip("% / interval")]
        [SerializeField] private float percentage;
        [Tooltip("% lost / operation")]
        [SerializeField] private float percentageloss = 0;
        [Tooltip("Number of operations required")]
        [SerializeField] private int operations = 1;
        [Tooltip("Padding for red bar, in width/2")]
        [SerializeField] private int padding = 1;
        [Tooltip("Gap between the bars")]
        [SerializeField] private float gap = 0.5f;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private float interval = 1f;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform bar;
        [HideInInspector] private Transform red;
        [HideInInspector] private Transform red2;
        [HideInInspector] private int opsdone = 0;
        [HideInInspector] private bool goright = true;
        [HideInInspector] private Coroutine cooldownroutine;
        [HideInInspector] private bool cd = false;
        [HideInInspector] private bool started = false;

        public bool Isfilled()
        {
            if (opsdone == operations)
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

        public void Clickme()
        {
            if (Isinzone() && !cd)
            {
                opsdone++;
                cd = true;
                cooldownroutine = StartCoroutine("Cooldown");
            }
        }

        private IEnumerator Addprogress()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                if (goright)
                {
                    progress.localPosition += new Vector3(percentage, red.localPosition.y);

                    if (progress.localPosition.x + progress.localScale.x / 2 >= bar.localScale.x / 2f)
                        goright = false;
                }
                else if (!goright)
                {
                    progress.localPosition -= new Vector3(percentage, red.localPosition.y);

                    if (progress.localPosition.x - progress.localScale.x / 2 <= -bar.localScale.x / 2f)
                        goright = true;
                }

                if (Isfilled())
                    break;
            }
        }

        private bool Isinzone()
        {
            if (progress.localPosition.x > red.localPosition.x && progress.localPosition.x < red2.localPosition.x)
                return true;
            return false;
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(cooldown);
            cd = false;

        }

        private void Redloc()
        {
            float width = red.localScale.x / 2f;
            float rand = Random.Range(padding * width + gap + width * 2 - bar.localScale.x / 2f, bar.localScale.x / 2f - width * 2 - gap - padding * width);
            red.localPosition = new Vector3(rand, red.localPosition.y);
            red2.localPosition = new Vector3(rand + gap, red.localPosition.y);
        }

        private void Awake()
        {
            progress = transform.Find("Progress");
            bar = transform.Find("Bar");
            red = transform.Find("Red");
            red2 = transform.Find("Red2");

            percentage *= bar.localScale.x / 100f;
            percentageloss *= bar.localScale.x / 100f;

            Redloc();
        }
    }
}