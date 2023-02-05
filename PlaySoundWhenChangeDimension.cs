using System;
using UnityEngine;

public class PlaySoundWhenChangeDimension : MonoBehaviour, IObserver<Dimension>
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string soundId;

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
        audioManager.PlaySound(soundId);
    }
}
