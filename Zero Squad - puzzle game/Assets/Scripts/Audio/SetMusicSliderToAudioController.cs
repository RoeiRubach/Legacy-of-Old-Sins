using UnityEngine;
using UnityEngine.UI;

public class SetMusicSliderToAudioController : MonoBehaviour
{
    private void Start() => MusicAudioController.Instance.SetMusicController(GetComponent<Slider>());
}
