public class Console : InteractableBase, IHectorInteractables
{
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
