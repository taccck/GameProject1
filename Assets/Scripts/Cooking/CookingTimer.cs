using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CookingTimer : MonoBehaviour
{
    [SerializeField] private Text timer;

    public void SetUI(float time)
    {
        timer.text = Mathf.Ceil(time).ToString(CultureInfo.InvariantCulture);
    }

    private void Awake()
    {
        timer = GetComponentInChildren<Text>();
    }
}