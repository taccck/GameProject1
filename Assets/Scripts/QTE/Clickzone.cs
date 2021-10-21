using System.Collections;
using UnityEngine;

namespace FG
{
    public class Clickzone : QTE
    {
        private bool goright = true;
        private Coroutine progressRoutine;
        private LayerMask minigameMask;

        public override bool Isfilled()
        {
            return done;
        }

        public override bool Startbar()
        {
            StartCoroutine(Addprogress());
            return base.Startbar();
        }

        public override void Interact()
        {
            if (!cd)
            {
                RaycastHit2D hit = Physics2D.Raycast(progress.position, Vector2.up, 1f, minigameMask);
                if (hit.collider != null && hit.collider.CompareTag("Progbar"))
                {
                    done = true;
                    StopCoroutine(progressRoutine);
                }
                StartCoroutine(Cooldown());
            }
        }

        protected override IEnumerator Addprogress()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

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

                if (done)
                    break;
            }
        }

        protected override void Redloc()
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
            minigameMask = LayerMask.GetMask("Progbar");
        }
    }
}