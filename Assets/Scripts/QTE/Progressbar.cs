using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Progressbar : MonoBehaviour
    {
        [Tooltip("% / operation")]
        [SerializeField] private float percentage;

        [HideInInspector] private Transform progress;
        [HideInInspector] private Transform bar;

        public bool Isfilled()
        {
            if (progress.localScale.x >= bar.localScale.x)
                return true;
            return false;
        }

        public void Addprogress()
        {
            progress.localScale += new Vector3(percentage, 0);
            progress.localPosition = new Vector3((-bar.localScale.x / 2) + progress.localScale.x / 2, progress.localPosition.y);
        }

        private void Awake()
        {
            progress = transform.Find("Progress");
            bar = transform.Find("Bar");

            progress.localScale = new Vector3(0, progress.localScale.y);

            percentage *= bar.localScale.x / 100;
        }
    }
}