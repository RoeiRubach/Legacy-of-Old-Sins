using UnityEngine;

public class HectorSFX : CharacterSFXBase
{
    [SerializeField] private AudioClip _hectorFirstEncounter;
    [SerializeField] private AudioClip _hectorShieldOff;
    [SerializeField] private AudioClip _activationSequenceComplete;

    public void PlayHectorFirstEncounter() => audioSource.PlayOneShot(_hectorFirstEncounter);

    public void PlayHectorShieldOFF()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_hectorShieldOff);
    }

    public void PlayActivationSequenceComplete()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_activationSequenceComplete);
    }
}