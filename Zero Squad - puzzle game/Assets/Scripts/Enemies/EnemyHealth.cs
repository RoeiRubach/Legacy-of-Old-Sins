using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private Slider _slider;

    private Camera _mainCam;

    private bool _isSet, _isKilled;

    private void Start()
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
                _mainCam = Camera.main;
            }
            _healthBarUI.transform.LookAt(_mainCam.transform);
        }
    }

    private float CalculateHealth() => _currentHealth / _maxHealth;

    public void HealthDecreaseViaBullet()
    {
        _currentHealth -= 1;

        if (_currentHealth <= 0)
        {
            GetComponentInParent<BoxCollider>().enabled = false;
            _isKilled = true;
            _healthBarUI.SetActive(false);
            GetComponent<GameEventSubscriber>()?.OnEventFire();
        }
    }

    public bool CheckIfEnemyDead() => _isKilled;
    public float GetCurrentHealth() => _currentHealth;
}
