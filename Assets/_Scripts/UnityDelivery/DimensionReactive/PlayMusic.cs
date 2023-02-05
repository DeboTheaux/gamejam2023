using System;
using UnityEngine;

public class PlayMusic : MonoBehaviour, IObserver<Dimension>
{
    [SerializeField] private AudioManager audioManager;

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Dimension value)
    {
        if (value is RealityDimension)
        {
            audioManager.PlayMusic("Nivel1");
        }
        else
        {
            audioManager.PlayMusic("Nivel2");
        }
    }
}
