using UnityEngine;

public class CookingInputDisplay : MonoBehaviour
{
   [SerializeField] private Sprite[] wasdSprites;

   private SpriteRenderer spriteRenderer;

   public void SetUI(Vector2 button)
   {
      if (Mathf.Approximately(button.y, 1f))
         spriteRenderer.sprite = wasdSprites[0];
      else if(Mathf.Approximately(button.x, -1f))
         spriteRenderer.sprite = wasdSprites[1];
      else if(Mathf.Approximately(button.y, -1f))
         spriteRenderer.sprite = wasdSprites[2];
      else if(Mathf.Approximately(button.x, 1f))
         spriteRenderer.sprite = wasdSprites[3];
   }

   private void Awake()
   {
      spriteRenderer = GetComponent<SpriteRenderer>();
   }
}
