using System;
using UnityEngine;

public class PlayAnimation : PlayerAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTrigger;

    public override void Execute(Action OnComplete)
    {
        animator.SetTrigger(animationTrigger);
        OnComplete();
    }
}
