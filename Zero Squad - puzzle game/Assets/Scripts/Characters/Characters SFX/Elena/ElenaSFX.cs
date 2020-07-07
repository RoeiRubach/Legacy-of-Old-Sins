using UnityEngine;

public class ElenaSFX : CharacterSFXBase
{
    [SerializeField] private AudioClip _elenaFirstEncounter;
    [SerializeField] private AudioClip _elenaBackstab;

    public void PlayElenaFirstEncounter() => audioSource.PlayOneShot(_elenaFirstEncounter);

    public void PlayElenaBackstab()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_elenaBackstab);
    }
}
