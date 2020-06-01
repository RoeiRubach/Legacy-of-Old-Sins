using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth = 10;

    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isShield;

    private Camera _mainCam;

    private GameObject _shieldRef;
    private bool _isSet, _isKilled;
    private float _regenerateTimer = 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void LateUpdate()
    {
        if (_slider == null)
            return;

        _slider.value = Mathf.Lerp(_slider.value, CalculateHealth(), 0.55f);

        if (_currentHealth < _maxHealth)
        {
            if (!_isSet)
            {
                _isSet = true;
                _healthBarUI.SetActive(true);
                if(_mainCam == null)
                    _mainCam = Camera.main;
            }
            _healthBarUI.transform.LookAt(_mainCam.transform);
        }
    }

    private float CalculateHealth() => (float)_currentHealth / _maxHealth;

    public void HealthDecreaseViaBullet()
    {
        _currentHealth -= 1;

        if (_currentHealth <= 0)
        {
            if (_isShield)
            {
                ToggleHealthBarOFF();
                GetComponent<GameEventSubscriber>()?.OnEventFire();
            }
            else
            {
                GetComponentInParent<BoxCollider>().enabled = false;
                _isKilled = true;
                ToggleHealthBarOFF();
                GetComponent<GameEventSubscriber>()?.OnEventFire();
            }
        }
    }

    public void ShieldHealthRegenerate()
    {
        _isSet = false;
        ToggleHealthBarOFF();
        _currentHealth += 1;
    }

    private void ToggleHealthBarOFF()
    {
        if (_healthBarUI != null && _healthBarUI.activeSelf)
            _healthBarUI.SetActive(false);
    }

    public bool CheckIfEnemyDead => _isKilled;
    public int GetCurrentHealth => _currentHealth;
    public int GetMaxHealth => _maxHealth;
}
