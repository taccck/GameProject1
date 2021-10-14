using System.Collections;
using UnityEngine;

namespace FG
{
    public class Clickzone : MonoBehaviour
    {
        [Tooltip("% / interval")] [SerializeField]
        private float percentage;

        [Tooltip("Padding for red bar, in width/2")] [SerializeField]
        private int padding = 1;

        [SerializeField] private float interval = 1f;
        [SerializeField] private float cooldown = 1f;

        private Transform progress;
        private Transform bar;
        private Transform red;
        private bool goright = true;
        private bool started = false;
        private bool done = false;
        private Vector2 input = Vector2.zero;
        private bool cd = false;
        private Coroutine progressRoutine;
        private LayerMask minigameMask;

        public bool Isfilled()
        {
            return done;
        }

        public bool Startbar()
        {
            if (!started)
            {
                started = true;
                progressRoutine = StartCoroutine("Addprogress");
                return true;
            }

            return false;
        }

        public void Interact()
        {
            if (!cd)
            {
                RaycastHit2D hit = Physics2D.Raycast(progress.position, Vector2.up, 1f, minigameMask);
                if (hit.collider != null && hit.collider.CompareTag("Progbar"))
                {
                    done = true;
                    StopCoroutine(progressRoutine);
                }
                StartCoroutine("Cooldown");
            }
        }

        public Vector2 Checkinput()
        {
            if (input == Vector2.zero)
                Generateinput();
            return input;
        }

        private IEnumerator Cooldown()
        {
            cd = true;
            yield return new WaitForSeconds(cooldown);
            cd = false;
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
            minigameMask = LayerMask.GetMask("Progbar");
        }
    }
}