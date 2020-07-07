using UnityEngine;

public class SwitchSFX : MonoBehaviour
{
    [SerializeField] private AudioClip _successClip;
    [SerializeField] private AudioClip _errorDetectedClip;
    [SerializeField] private AudioClip _switchONClip;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void PlayErrorClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_errorDetectedClip);
    }

    public void PlaySuccessClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_successClip);
    }

    public void PlaySwitchONClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_switchONClip);
    }
}