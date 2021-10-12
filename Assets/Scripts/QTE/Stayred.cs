using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stayred : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("stay");
    }
}
