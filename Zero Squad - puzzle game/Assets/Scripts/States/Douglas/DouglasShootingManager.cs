using UnityEngine;

public class DouglasShootingManager : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletHolder;

    public GameObject DouglasShotgunRef;

    private GameObject _bulletClone;
    private float _shootingDelay = 0.875f;

    private bool _isAllowedToShoot;

    public bool CallSimpleShoot()
    {
        if (_isAllowedToShoot)
        {
            _bulletClone = Instantiate(_bullet, _bulletHolder.transform);
            _bulletClone.transform.parent = null;

            _shootingDelay = 1.2f;
            _isAllowedToShoot = false;
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (!_isAllowedToShoot)
        {
            _shootingDelay -= Time.deltaTime;

            if (_shootingDelay <= 0)
                _isAllowedToShoot = true;
        }
    }

    private void OnDisable()
    {
        _isAllowedToShoot = false;
        _shootingDelay = 0.875f;
    }
}
