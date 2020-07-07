using UnityEngine;
using UnityEngine.UI;

public class SFXAudioCotroller : SingletonDontDestroy<SFXAudioCotroller>
{
    private AudioSource _audioSource;
    private Slider _sfxSliderController;
    private float _currentAudioVolume;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        _currentAudioVolume = _audioSource.volume;
    }

    public float GetSFXAudioVolume() => _audioSource.volume;

    public void SetSFXController(Slider sfxSliderController)
    {
        _sfxSliderController = sfxSliderController;
        sfxSliderController.value = _audioSource.volume;
    }

    private void LateUpdate()
    {
        if (_sfxSliderController == null) return;

        _audioSource.volume = _sfxSliderController.value;
        _currentAudioVolume = _audioSource.volume;
    }
}
