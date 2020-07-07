using UnityEngine;

public class CharacterSFXBase : MonoBehaviour
{
    [SerializeField] protected AudioClip[] _characterDeaths;
    [SerializeField] protected AudioClip[] _characterHits;
    [SerializeField] protected AudioClip[] _characterSelecteds;
    [SerializeField] protected AudioClip[] _characterSkillModeSwitching;
    [SerializeField] protected AudioClip _collectHealthPack;

    protected AudioSource audioSource;

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void LateUpdate() => audioSource.volume = SFXAudioCotroller.Instance.GetSFXAudioVolume();

    public void PlayRandomDeathClip()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_characterDeaths[Random.Range(0, _characterDeaths.Length)]);
    }

    public void PlayRandomHitClip()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_characterHits[Random.Range(0, _characterHits.Length)]);
    }

    public void PlayRandomSelectedClip()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_characterSelecteds[Random.Range(0, _characterSelecteds.Length)]);
    }

    public void PlayRandomSkillClip()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_characterSkillModeSwitching[Random.Range(0, _characterSkillModeSwitching.Length)]);
    }

    public void PlayCollectHealthPackClip()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(_collectHealthPack);
    }
}