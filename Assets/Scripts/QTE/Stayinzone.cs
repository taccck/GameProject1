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
        [HideInInspector] private Vector2 input = Vector2.zero;

        public bool Isfilled()
        {
            if (Isinzone())
                return true;
            return false;
        }

        public bool Startbar()
        {
            return true;
        }

        public void Interact()
        {
            if (!cd)
            {
                progress.localPosition += new Vector3(percentage, 0);
                StartCoroutine("Cooldown");
            }
        }

        public Vector2 Checkinput()
        {
            if (input == Vector2.zero)
                Generateinput();
            return input;
        }

        private void Generateinput()
        {
            int dir = Random.Range(0, 4);
            if (dir == 0)
                input.x = 1;
            else if (dir == 1)
                input.x = -1;
            else if (dir == 2)
                input.y = 1;
            else if (dir == 3)
                input.y = -1;
        }

        private IEnumerator Addprogress()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                progress.localPosition -= new Vector3(percentage, 0);
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
            cd = true;
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

            StartCoroutine("Addprogress");
        }
    }
}