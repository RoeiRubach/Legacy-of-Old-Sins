using UnityEngine;

public class RegenerateShield : MonoBehaviour
{
    [SerializeField] private EnemyHealth _ShieldHealth;
    [Range(0.5f, 2f)]
    [SerializeField] private float _regenerateHealthTimer = 0.5f;
    private GameObject _shieldRef;
    private float _regenerateTimer;
    private void Start()
    {
        _shieldRef = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!_shieldRef.activeInHierarchy)
        {
            if (_ShieldHealth.GetCurrentHealth < _ShieldHealth.GetMaxHealth)
            {
                if ((_regenerateTimer += Time.deltaTime) > _regenerateHealthTimer)
                {
                    _regenerateTimer = 0;
                    _ShieldHealth.ShieldHealthRegenerate();
                }
            }
        }
    }
}
