using UnityEngine;

public class HealthRegenCollectables : InteractableBase, IDouglasInteractables, IElenaInteractables, IHectorInteractables
{
    [Range(1, 10)]
    [SerializeField] private int _healthToRegen = 10;

    public int HealthToRegen => _healthToRegen;

    public void CallOnDestroy()
    {
        GetComponent<GameEventSubscriber>()?.OnEventFire();
        Destroy(GetComponent<HealthRegenCollectables>());
        Destroy(GetComponent<Outline>());
        Destroy(gameObject);
    }
}
