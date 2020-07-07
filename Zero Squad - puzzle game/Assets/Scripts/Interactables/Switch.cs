using UnityEngine;

public class Switch : InteractableBase, IDouglasInteractables, IHectorInteractables, IElenaInteractables
{
    private SwitchSFX _switchSFX;

    private void Awake() => _switchSFX = GetComponent<SwitchSFX>();

    public override void Interact()
    {
        if (GetComponent<GameEventSubscriber>().isActiveAndEnabled)
        {
            PlaySuccessClip();
            GetComponent<GameEventSubscriber>()?.OnEventFire();
            Destroy(GetComponent<Switch>());
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            if(_switchSFX != null)
                _switchSFX.PlayErrorClip();
        }
    }

    public void SetSwitchOn()
    {
        PlaySwitchONClip();
        GetComponent<GameEventSubscriber>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void PlaySuccessClip()
    {
        if (_switchSFX != null)
            _switchSFX.PlaySuccessClip();
    }

    private void PlaySwitchONClip()
    {
        if (_switchSFX != null)
            _switchSFX.PlaySwitchONClip();
    }
}
