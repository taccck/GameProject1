using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    private Image image;

    public void SetUI(Item item)
    {
        if (item == null)
        {
            image.color = new Color(0, 0, 0, 0);
            return;
        }

        image.color = new Color(1, 1, 1, 1);
        image.sprite = item.sprite;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }
}