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