using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicAudioController : SingletonDontDestroy<MusicAudioController>
{
    private const float FADEIN_TIME = 4f;
    private const float FADEOUT_TIME = 2f;

    public int MusicToPlay { get; private set; }
    [SerializeField] private AudioClip _tutorialMusic;
    [SerializeField] private AudioClip _firstRoomMusic;
    [SerializeField] private AudioClip _thirdRoomMusic;

    private AudioSource _audioSource;
    private Slider _musicSliderController;
    private float _currentAudioVolume;
    private bool _isFading;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        _currentAudioVolume = _audioSource.volume;
    }

    private void LateUpdate()
    {
        if (_isFading || _musicSliderController == null) return;

        _audioSource.volume = _musicSliderController.value;
        _currentAudioVolume = _audioSource.volume;
    }

    public void SetMusicController(Slider musicSliderController)
    {
        _musicSliderController = musicSliderController;
        musicSliderController.value = _audioSource.volume;
    }

    public void SwitchGameMusic(int musicToPlay = 0)
    {
        StopAllCoroutines();
        MusicToPlay = musicToPlay;
        print(MusicToPlay);
        StartCoroutine(FadeOutMusic());
    }

    private void PlayAudioMusic()
    {
        switch (MusicToPlay)
        {
            case 1:
                SetAudioAsFirstRoomMusic();
                break;
            case 2:
            case 3:
                SetAudioAsThirdRoomMusic();
                break;
            default:
                SetAudioAsTutorialMusic();
                break;
        }
    }

    private void SetAudioAsTutorialMusic()
    {
        _audioSource.clip = _tutorialMusic;
        PlayAudioWithFadeIn();
    }

    private void SetAudioAsFirstRoomMusic()
    {
        _audioSource.clip = _firstRoomMusic;
        PlayAudioWithFadeIn();
    }

    private void SetAudioAsThirdRoomMusic()
    {
        _audioSource.clip = _thirdRoomMusic;
        PlayAudioWithFadeIn();
    }

    private void PlayAudioWithFadeIn()
    {
        _audioSource.Play();
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        _audioSource.volume = 0;

        while (_audioSource.volume < _currentAudioVolume)
        {
            _audioSource.volume += _currentAudioVolume * (Time.deltaTime / FADEIN_TIME);
            yield return null;
        }

        _audioSource.volume = _currentAudioVolume;
        _isFading = false;
    }

    private IEnumerator FadeOutMusic()
    {
        _currentAudioVolume = _audioSource.volume;
        _isFading = true;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= _currentAudioVolume * Time.deltaTime;
            yield return null;
        }

        _audioSource.volume = 0;
        PlayAudioMusic();
    }
}