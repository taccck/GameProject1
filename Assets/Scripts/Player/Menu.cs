using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject menuToggle;

    private bool active = false;

    private void OnMenu()
    {
        active = !active;
        inventory.SetActive(!active);
        menuToggle.SetActive(active);
        Time.timeScale = active ? 0f : 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetLevel()
    {
        SaveManager.Curr.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnMenu();
    }
}