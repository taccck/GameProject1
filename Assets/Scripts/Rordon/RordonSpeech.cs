using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RordonSpeech : MonoBehaviour
{
    [SerializeField] private float betweenCharacters = .1f;
    [SerializeField] private string[] lines;
    [SerializeField] private Text text;
    private bool running;

    public void StartRordonLine()
    {
        if (!running) StartCoroutine(RordonLine());
    }

    private IEnumerator RordonLine()
    {
        running = true;
        transform.GetChild(0).gameObject.SetActive(true);
        
        text.text = "";
        int randomIndex = Random.Range(0, lines.Length - 1);
        string line = lines[randomIndex];
        foreach (char c in line)
        {
            text.text += c;
            yield return new WaitForSeconds(betweenCharacters);
        }

        yield return new WaitForSeconds(betweenCharacters * 10);

        transform.GetChild(0).gameObject.SetActive(false);
        running = false;
    }
}