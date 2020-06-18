using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 3;
    public bool IsKilled
    {
        get;
        private set;
    }
    public int CurrentHealth
    {
        get;
        private set;
    }

    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isShield;

    private Camera _mainCam;
    private GameObject _shieldRef;
    private bool _isSet;
    private float _regenerateTimer = 0;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    private void LateUpdate()
    {
        if (_slider == null)
            return;

        _slider.value = Mathf.Lerp(_slider.value, CalculateHealth(), 0.55f);

        if (CurrentHealth < MaxHealth)
        {
            if (!_isSet)
            {
                _isSet = true;
                _healthBarUI.SetActive(true);
                if (_mainCam == null)
                    _mainCam = Camera.main;
            }
            _healthBarUI.transform.LookAt(_mainCam.transform);
        }
    }

    private float CalculateHealth() => (float)CurrentHealth / MaxHealth;

    public void HealthDecreaseViaBullet(int damageAmount = 1)
    {
        for (int i = 0; i < damageAmount; i++)
        {
            CurrentHealth--;
            if (CurrentHealth <= 0)
            {
                if (_isShield)
                {
                    ToggleHealthBarOFF();
                    GetComponent<GameEventSubscriber>()?.OnEventFire();
                }
                else
                {
                    GetComponentInParent<BoxCollider>().enabled = false;
                    IsKilled = true;
                    ToggleHealthBarOFF();
                    if (transform.parent != null)
                        transform.parent = null;
                    GetComponent<GameEventSubscriber>()?.OnEventFire();
                }
            }
        }
    }

    public void ShieldHealthRegenerate()
    {
        _isSet = false;
        ToggleHealthBarOFF();
        CurrentHealth += 1;
    }

    private void ToggleHealthBarOFF()
    {
        if (_healthBarUI != null && _healthBarUI.activeSelf)
            _healthBarUI.SetActive(false);
    }

    public void AssassinateEnemy()
    {
        CurrentHealth = 0;
        HealthDecreaseViaBullet();
    }
}
