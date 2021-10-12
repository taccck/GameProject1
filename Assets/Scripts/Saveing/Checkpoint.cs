using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool reached;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (reached) return;
        
        SaveManager.Curr.Save();
        reached = true;
    }

    private void Start()
    {
        if(!SaveManager.Curr.saveFile.first)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}