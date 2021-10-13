using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Progressbar : MonoBehaviour
    {
        [Tooltip("% / interval")]
        [SerializeField] private float percentage;
        [SerializeField] private float interval = 1f;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform bar;
        [HideInInspector] private bool started = false;
        [HideInInspector] private Vector2 input = Vector2.zero;

        public bool Isfilled()
        {
            if (progress.localScale.x >= bar.localScale.x)
                return true;
            return false;
        }

        public bool Startbar()
        {
            if(!started)
            {
                started = true;
                StartCoroutine("Addprogress");
                return true;
            }
            return false;
        }

        public void Interact()
        {
            return;
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

                progress.localScale += new Vector3(percentage, 0);
                progress.localPosition = new Vector3((-bar.localScale.x / 2f) + progress.localScale.x / 2f, progress.localPosition.y);
                
                if (Isfilled())
                    break;
            }
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