using UnityEngine;

public class SetMusicToRoomAudio : MonoBehaviour
{
    [SerializeField] private int _musicToPlay;

    public void SetAudioMusic() => MusicAudioController.Instance.SwitchGameMusic(_musicToPlay);
}
