using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HealthRegenCollectables : MonoBehaviour
{
    [SerializeField] private int healthToRegen;

    public int HealthToRegen => healthToRegen;

    public void CallOnDestroy()
    {
        Destroy(gameObject);
    }
}
