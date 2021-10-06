using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public readonly int Idle = Animator.StringToHash("Idle");
    public readonly int Jump = Animator.StringToHash("Jump");

    private Animator animator;
    private int currState;
    
    public void ChangeAnimationState(int newState)
    {
        if (currState == newState)
            return;

        animator.Play(newState);
        currState = newState;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}