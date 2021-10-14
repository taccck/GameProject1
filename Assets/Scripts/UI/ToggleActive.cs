using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    [SerializeField] private GameObject toToggle;
    private bool active;

    public void Toggle()
    {
        active = !active;
        toToggle.SetActive(active);
    }
}
