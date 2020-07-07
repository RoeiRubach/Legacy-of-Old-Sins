using UnityEngine;

public class MindlessShooterSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] _deathClips;
    [SerializeField] private AudioClip[] _hitClips;
    [SerializeField] private AudioClip[] _lostElenaClips;
    [SerializeField] private AudioClip[] _elenaDetecedClips;
    [SerializeField] private AudioClip[] _characterTriggerClips;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void LateUpdate() => _audioSource.volume = SFXAudioCotroller.Instance.GetSFXAudioVolume();

    public void PlayRandomDeathClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_deathClips[Random.Range(0, _deathClips.Length)]);
    }

    public void PlayRandomHitClip() => _audioSource.PlayOneShot(_hitClips[Random.Range(0, _hitClips.Length)]);

    public void PlayRandomLostElenaClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_lostElenaClips[Random.Range(0, _lostElenaClips.Length)]);
    }

    public void PlayRandomDetecedElenaClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_elenaDetecedClips[Random.Range(0, _elenaDetecedClips.Length)]);
    }

    public void PlayRandomCharacterTriggerClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_characterTriggerClips[Random.Range(0, _characterTriggerClips.Length)]);
    }
}