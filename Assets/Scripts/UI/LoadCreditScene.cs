using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCreditScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Credscene", LoadSceneMode.Single);
    }
}
