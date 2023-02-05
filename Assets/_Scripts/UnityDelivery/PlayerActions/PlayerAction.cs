using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] private Image activeIndicator;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string soundIdExecute;
    [SerializeField] private string soundIdActive;
    [SerializeField] private string soundIdInactive;

    public virtual void Execute(Action OnComplete)
    {
        SetActive();
        audioManager.PlaySound(soundIdExecute);
    }

    public void SetActive()
    {
        audioManager.PlaySound(soundIdActive);
        activeIndicator.gameObject.SetActive(true);
    }

    public void SetInactive()
    {
        audioManager.PlaySound(soundIdInactive);
        activeIndicator.gameObject.SetActive(false);
    }
}
