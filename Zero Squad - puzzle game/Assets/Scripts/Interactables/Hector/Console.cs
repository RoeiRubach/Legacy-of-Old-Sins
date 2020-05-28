public class Console : InteractableBase, IHectorTech, IHectorInteractables
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
