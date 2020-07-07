using UnityEngine;

public class SummonerSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] _summonClips;
    [SerializeField] private AudioClip _deathClip;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void LateUpdate() => _audioSource.volume = SFXAudioCotroller.Instance.GetSFXAudioVolume();

    public void PlayRandomSummonClip() => _audioSource.PlayOneShot(_summonClips[Random.Range(0, _summonClips.Length)]);

    public void PlayDeathClip()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_deathClip);
    }
}
