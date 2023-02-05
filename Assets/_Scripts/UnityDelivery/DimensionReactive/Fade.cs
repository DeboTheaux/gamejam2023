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
        if (value is ConsciousDimension) StartCoroutine(Translate());
    }

    IEnumerator Translate()
    {
        while (transform.position.y >= -20)
        {
            transform.Translate(Vector3.down * 5f * Time.deltaTime);
            yield return null;
        }        
    }
}
