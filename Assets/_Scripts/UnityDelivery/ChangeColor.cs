using System;
using UnityEngine;

public class ChangeColor : PlayerAction
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Color newColor;

    public override void Execute(Action OnComplete)
    {
        renderer.material.color = newColor;
        OnComplete();
    }
}
