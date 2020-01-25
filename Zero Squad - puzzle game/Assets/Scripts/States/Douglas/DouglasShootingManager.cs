using UnityEngine;

public class DouglasShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    private GameObject _bulletClone;

    [SerializeField] private Transform _bulletHolder;

    private float _shootingDelay = 1f;

    private bool _isAllowedToShoot = true;

    public void CallSimpleShoot()
    {
        if (_isAllowedToShoot)
        {
            _bulletClone = Instantiate(_bullet, _bulletHolder.transform);
            _bulletClone.transform.parent = null;

            _shootingDelay = 1f;
            _isAllowedToShoot = false;
        }
    }

    public void Update()
    {
        if (!_isAllowedToShoot)
        {
            _shootingDelay -= Time.deltaTime;

            if (_shootingDelay <= 0)
            {
                _isAllowedToShoot = true;
            }
        }
    }
}
