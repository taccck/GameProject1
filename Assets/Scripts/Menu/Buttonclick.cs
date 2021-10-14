using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FG
{
    public class Buttonclick : MonoBehaviour
    {
        public void Playclick()
        {
            SceneManager.LoadScene("Complete Level");
        }

        public void Creditclick()
        {
            Debug.Log("creds");
        }

        public void Backclick()
        {
            Debug.Log("creds");
        }

        public void Quitclick()
        {
            Application.Quit();
        }
    }
}