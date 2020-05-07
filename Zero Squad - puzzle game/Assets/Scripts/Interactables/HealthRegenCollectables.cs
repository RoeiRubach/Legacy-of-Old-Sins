using UnityEngine;

public class HealthRegenCollectables : InteractableBase, IDouglasInteractables, IElenaInteractables, IHectorInteractables
{
    [SerializeField] private int healthToRegen;

    public int HealthToRegen => healthToRegen;

    public void CallOnDestroy()
    {
        GetComponent<GameEventSubscriber>()?.OnEventFire();
        isInteract = false;
        Destroy(gameObject);
    }
}
