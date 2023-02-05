using System;
using System.Collections;
using UnityEngine;

public class PlayAnimation : PlayerAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTrigger;
    [SerializeField] private GameObject onCompleteActiveObject;
    [SerializeField] private GameObject onCompleteActiveObject1;

    public override void Execute(Action OnComplete)
    {
        SetInactive();
        animator.SetTrigger(animationTrigger);
        StartCoroutine(Animation(OnComplete));
    }

    IEnumerator Animation(Action OnComplete)
    {
        yield return new WaitForSeconds(2f);

        if (onCompleteActiveObject != null) onCompleteActiveObject.SetActive(true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        if (onCompleteActiveObject1 != null)
        {
            onCompleteActiveObject.SetActive(false);
            onCompleteActiveObject1.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        OnComplete();
    }
}
