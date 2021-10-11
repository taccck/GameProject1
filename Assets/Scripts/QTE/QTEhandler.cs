using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class QTEhandler : MonoBehaviour
    {
        [SerializeField] private Progressbar bar;
        [SerializeField] private Progresscircle circle;

        private IEnumerator Updatebar()
        {
            while (true)
            {
                bar.Addprogress();
                if (bar.Isfilled())
                    break;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator Updatecircle()
        {
            while (true)
            {
                circle.Addprogress();
                if (circle.Isfilled())
                    break;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void Start()
        {
            StartCoroutine("Updatebar");
            StartCoroutine("Updatecircle");
        }
    }
}