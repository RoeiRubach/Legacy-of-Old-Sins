using UnityEngine;

public class DouglasShootingManager : MonoBehaviour
{
    private const float _manualShootingDelay = 1.1f;
    
    [SerializeField] private Transform _bulletHolder;

    public GameObject DouglasShotgunRef;
    public LayerMask AvoidLayersMasks;
    public LayerMask EnemyLayerMask;
    
    private float _shootingDelay = 0.875f;

    private bool _isAllowedToShoot;

    public bool CallSimpleShoot()
    {
        if (_isAllowedToShoot)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, EnemyLayerMask))
            {
                if (hitInfo.collider.transform.CompareTag("Enemy"))
                {
                    if (IsHavingClearShoot(hitInfo.transform))
                    {
                        DouglasShooting(hitInfo.transform);
                    }
                }
            }

            _shootingDelay = _manualShootingDelay;

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
        _shootingDelay = 0.8f;
    }

    private void DouglasShooting(Transform nearestEnemy)
    {
        nearestEnemy.GetComponent<EnemyHealth>().HealthDecreaseViaBullet();
        //transform.GetComponent<Animator>().SetBool("_isShooting", true);
    }

    private bool IsHavingClearShoot(Transform target)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position + (Vector3.up * 1.2f), DirectionToEnemy(target), out hitInfo, 10f, ~AvoidLayersMasks))
        {
            if (hitInfo.transform == target.transform)
                return true;
        }
        return false;
    }

    private Vector3 DirectionToEnemy(Transform target) => (target.position - transform.position).normalized;
}
