using UnityEngine;

public class CookingController : MonoBehaviour
{
    [SerializeField] private Sprite success;
    [SerializeField] private Sprite fail;
    [SerializeField] private SpriteRenderer toSwap;
    [SerializeField] private SpriteRenderer toHide;
    [SerializeField] private Animator toStop;

    public void Outcome(bool? outcome)
    {
        if (outcome == null) return;
        
        if (toHide != null)
            toHide.color = new Color(0, 0, 0, 0);
        toStop.enabled = false;
        
        if (fail == null || success == null) return;
        
        bool nonNullOutcome = outcome.Value;
        toSwap.sprite = nonNullOutcome ? success : fail;
        
    }
}