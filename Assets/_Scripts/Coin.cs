using System;
using UnityEngine;

public class Coin : MonoBehaviour, IObserver<Life>
{
    //ganar o perder
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    //reaccionar
    public void OnNext(Life value)
    {
        throw new NotImplementedException();
    }
}

