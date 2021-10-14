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
            SceneManager.LoadScene("FinalFinalCompleteScene");
        }

        public void Creditclick()
        {
            SceneManager.LoadScene("Credscene");
        }

        public void Backclick()
        {
            SceneManager.LoadScene("Mainmenu");
        }

        public void Quitclick()
        {
            Application.Quit();
        }
    }
}