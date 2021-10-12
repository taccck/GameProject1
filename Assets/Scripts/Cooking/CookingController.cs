using System;
using System.Collections;
using FG;
using UnityEngine;

public class CookingController : MonoBehaviour
{
    [Header("Event"), SerializeField] private float time;
    [SerializeField] private Progressbar progBar;
    [SerializeField] private Progresscircle progCircle;
    [SerializeField] private Stayinzone progZone; 

    
    [Header("Animation"), SerializeField] private Sprite success;
    [SerializeField] private Sprite fail;
    [SerializeField] private SpriteRenderer toSwap;
    [SerializeField] private SpriteRenderer toHide;
    [SerializeField] private Animator toStop;

    public void Outcome(bool outcome)
    {
        if (toHide != null)
            toHide.color = new Color(0, 0, 0, 0);
        toStop.enabled = false;
        
        if (fail == null || success == null) return;
        
        toSwap.sprite = outcome ? success : fail;
        
    }

    private IEnumerator CookTime()
    {
        yield return new WaitForSeconds(time);
        if (progBar != null)
        {
            Outcome(progBar.Isfilled());
        }
        else if (progCircle != null)
        {
            //Outcome(progCircle.Isfilled());
        }
        else if (progZone != null)
        {
            Outcome(progZone.Isfilled());
        }
    }

    private void Start()
    {
        StartCoroutine(CookTime());
    }
}