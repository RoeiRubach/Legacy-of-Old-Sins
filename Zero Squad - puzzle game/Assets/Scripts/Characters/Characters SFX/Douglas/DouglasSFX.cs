using UnityEngine;

public class DouglasSFX : CharacterSFXBase
{
    [SerializeField] private AudioClip _douglasBombCarring;
    [SerializeField] private AudioClip _douglasShotCast;

    public void PlayShotCast() => audioSource.PlayOneShot(_douglasShotCast);

    #region BOMB
    public void StartBombCarryingLoop() => InvokeRepeating("PlayBombCarrying", 0.25f, 3f);

    public void StopBombCarryingLoop() => CancelInvoke();

    private void PlayBombCarrying() => audioSource.PlayOneShot(_douglasBombCarring);
    #endregion
}
