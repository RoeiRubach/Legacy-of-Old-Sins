using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HectorShieldBreak : MonoBehaviour
{
    [SerializeField] private AudioClip _shieldBreak;
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void PlayBreakClip() => _audioSource.PlayOneShot(_shieldBreak);
}
