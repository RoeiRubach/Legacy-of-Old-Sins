using UnityEngine;

public class Console : InteractableBase, IHectorInteractables
{
    private void Update()
    {
        
    }

    public override void Interact()
    {
        GetComponent<GameEventSubscriber>()?.OnEventFire();
        Destroy(GetComponent<Console>());
        Destroy(GetComponent<Outline>());
    }

    public void Invoke()
    {
        GetComponent<Console>().enabled = true;
    }
}
