using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] private Image activeIndicator;

    public virtual void Execute(Action OnComplete)
    {
        SetInactive();
    }

    public void SetActive()
    {
        activeIndicator.gameObject.SetActive(true);
    }

    public void SetInactive()
    {
        activeIndicator.gameObject.SetActive(false);
    }
}
