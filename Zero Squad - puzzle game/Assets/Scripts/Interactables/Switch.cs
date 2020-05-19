using UnityEngine;

public class Switch : InteractableBase, IDouglasInteractables, IHectorInteractables, IElenaInteractables
{
    public override void Interact()
    {
        if (GetComponent<GameEventSubscriber>().isActiveAndEnabled)
        {
            // play correct sound
            GetComponent<GameEventSubscriber>()?.OnEventFire();
            Destroy(GetComponent<Switch>());
            //Destroy(GetComponent<Outline>());
        }
        else
        {
            print("SetSwitchOn Event haven't occurred yet");
            // play error sound
        }
    }

    public void SetSwitchOn()
    {
        GetComponent<GameEventSubscriber>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
