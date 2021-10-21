using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Progressbar : QTE
    {
        public override bool Isfilled()
        {
            if (progress.localScale.x >= bar.localScale.x)
                return true;
            return false;
        }

        public override void Interact()
        {
            if(!cd)
            {
                progress.localScale += new Vector3(percentage, 0);
                if (progress.localScale.x > 5) progress.localScale = new Vector2(5f, 1f);
                progress.localPosition = new Vector3((-bar.localScale.x / 2f) + progress.localScale.x / 2f,
                    progress.localPosition.y);

                StartCoroutine(Cooldown());
            }
        }

        public override Vector2 Checkinput()
        {
            if (input == Vector2.zero)
                Generateinput();
            return input;
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
            bar = transform.Find("Bar");

            progress.localScale = new Vector3(0, progress.localScale.y);

            percentage *= bar.localScale.x / 100f;
        }
    }
}