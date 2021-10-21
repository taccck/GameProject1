using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Progresscircle : QTE
    {
        [HideInInspector] private Transform circle;
        [HideInInspector] private bool swapped = false;

        public override bool Isfilled()
        {
            if (progress.localScale.x / circle.localScale.x == 1)
                return true;
            return false;
        }

        public override void Interact()
        {
            if (!cd)
            {
                progress.localScale += new Vector3(percentage, percentage);

                if (!swapped && progress.localScale.x > circle.localScale.x)
                {
                    circle.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    swapped = true;
                }

                if (progress.localScale.x > circle.localScale.x)
                    progress.GetComponent<SpriteRenderer>().color = Color.red;

                StartCoroutine(Cooldown());
            }
        }

        public override Vector2 Checkinput()
        {
            Generateinput();
            return input;
        }

        protected override void Generateinput()
        {
            if (input == Vector2.zero)
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
                Generateinput();
            }
            else
            {
                if (Mathf.Approximately(input.x, 1))
                {
                    input.x = 0;
                    input.y = -1;
                    return;
                }

                if (Mathf.Approximately(input.y, -1))
                {
                    input.x = -1;
                    input.y = 0;
                    return;
                }

                if (Mathf.Approximately(input.x, -1))
                {
                    input.x = 0;
                    input.y = 1;
                    return;
                }

                if (Mathf.Approximately(input.y, 1))
                {
                    input.x = 1;
                    input.y = 0;
                }
            }
        }

        protected override IEnumerator Addprogress()
        {
            yield return new WaitForSeconds(interval);
        }

        protected override void Redloc()
        {
            ;
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