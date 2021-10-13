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

        private void Start()
        {
            bar.Startbar();
            circle.Startbar();
            stay.Startbar();
            click.Startbar();
        }
    }
}