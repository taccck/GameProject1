using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class QTEhandler : MonoBehaviour
    {
        [SerializeField] private Progressbar bar;
        [SerializeField] private Progresscircle circle;
        [SerializeField] private Stayinzone stay;
        [SerializeField] private Clickzone click;

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
        private IEnumerator Updatestay()
        {
            while (true)
            {
                stay.Addprogress();
                if (stay.Isfilled())
                    break;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator Updateclick()
        {
            while (true)
            {
                click.Addprogress();
                if (click.Isinzone())
                    break;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void Start()
        {
            StartCoroutine("Updatebar");
            StartCoroutine("Updatecircle");
            StartCoroutine("Updatestay");
            StartCoroutine("Updateclick");
        }
    }
}