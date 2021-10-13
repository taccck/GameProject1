using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public readonly int Idle = Animator.StringToHash("Idle");
    public readonly int Jump = Animator.StringToHash("Jump");
    public readonly int Bonk = Animator.StringToHash("Bonk");
    public readonly int Dash = Animator.StringToHash("Dash");
    public readonly int Walk = Animator.StringToHash("Walk");

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