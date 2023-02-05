using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour, IObserver<Dimension>
{
    public void OnCompleted()
    {
       
    }

    public void OnError(Exception error)
    {
       
    }

    public void OnNext(Dimension value)
    {
        if (value is ConsciousDimension) gameObject.SetActive(false);
    }
}
