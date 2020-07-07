using UnityEngine;
using UnityEngine.UI;

public class SetSFXSliderToAudioController : MonoBehaviour
{
    private void Start() => SFXAudioCotroller.Instance.SetSFXController(GetComponent<Slider>());
}
