using System;
using System.Collections;
using UnityEngine;

public class PlayAnimation : PlayerAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTrigger;

    public override void Execute(Action OnComplete)
    {
        SetInactive();
        animator.SetTrigger(animationTrigger);
        StartCoroutine(Animation(OnComplete));
    }

    IEnumerator Animation(Action OnComplete)
    {
        yield return new WaitForSeconds(1f);
        OnComplete();
    }
}
