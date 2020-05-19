using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadGuyWithAKey : InteractableBase, IDouglasInteractables
{
    public override void Interact()
    {
        GetComponent<GameEventSubscriber>()?.OnEventFire();
        Destroy(GetComponent<DeadGuyWithAKey>());
        Destroy(GetComponent<Outline>());
    }
}
