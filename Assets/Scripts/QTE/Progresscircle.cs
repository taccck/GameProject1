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
        [SerializeField] private float cooldown = 1f;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform circle;
        [HideInInspector] private bool swapped = false;
        [HideInInspector] private bool started = false;
        [HideInInspector] private Vector2 input = Vector2.zero;
        [HideInInspector] private bool cd = false;

        public bool Isfilled()
        {
            if (progress.localScale.x / circle.localScale.x == 1)
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
                Addprogress();
                Generateinput();
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
            }
            else
            {
                if (input.x == 1)
                {
                    input.x = 0;
                    input.y = -1;
                }
                else if (input.y == -1)
                {
                    input.x = -1;
                    input.y = 0;
                }
                else if (input.x == -1)
                {
                    input.x = 0;
                    input.y = 1;
                }
                else if (input.y == 1)
                {
                    input.x = 1;
                    input.y = 0;
                }
            }
        }

        private void Addprogress()
        {
            if (!Isfilled())
            {
                progress.localScale += new Vector3(percentage, percentage);

                if (!swapped && progress.localScale.x > circle.localScale.x)
                {
                    circle.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    swapped = true;
                }
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