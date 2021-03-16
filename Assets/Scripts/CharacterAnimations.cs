using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    
    private static readonly int IdleTrigger = Animator.StringToHash("Idle");
    private static readonly int JumpTrigger = Animator.StringToHash("Jump");
    private static readonly int FinishTrigger = Animator.StringToHash("Finish");
    
    public void Idle()
    {
        characterAnimator.SetTrigger(IdleTrigger);
    }
    
    public void Jump()
    {
        characterAnimator.SetTrigger(JumpTrigger);
    }
    
    public void Finish()
    {
        characterAnimator.SetTrigger(FinishTrigger);
    }

    void ResetTriggers()
    {
        characterAnimator.ResetTrigger(IdleTrigger);
        characterAnimator.ResetTrigger(JumpTrigger);
        characterAnimator.ResetTrigger(FinishTrigger);
    }
}