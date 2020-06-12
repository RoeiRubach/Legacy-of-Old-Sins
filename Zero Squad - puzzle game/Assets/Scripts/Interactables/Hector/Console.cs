public class Console : InteractableBase, IHectorTech, IHectorInteractables
{
    public bool IsOneTimeUse = true;

    private void Start()
    {
        transform.name = "Console";
    }

    public override void Interact()
    {
        GetComponent<GameEventSubscriber>()?.OnEventFire();
        if (IsOneTimeUse)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GetComponent<Console>());
            Destroy(GetComponent<Outline>());
        }
    }

    public void Invoke()
    {
        GetComponent<Console>().enabled = true;
    }

    public void DestroyViaEvent()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(GetComponent<Console>());
        Destroy(GetComponent<Outline>());
    }
}
