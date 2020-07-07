using UnityEngine;

public class DouglasShootingManager : MonoBehaviour
{
    private const float MANUAL_SHOOTING_DELAY = 1.1f;

    [HideInInspector] public Transform DouglasTarget = null;
    public GameObject DouglasShotgunRef;
    [SerializeField] private Transform _bulletHolder;

    private float _shootingDelay = 0.875f;
    private PlayerController _playerController;
    private bool _isAllowedToShoot;

    private void Awake() => _playerController = FindObjectOfType<PlayerController>();

    public bool IsHavingClearShoot(Transform target)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position + (Vector3.up * 1.2f), DirectionToEnemy(target), out hitInfo, 10f, ~_playerController.AvoidLayersMasks))
        {
            if (hitInfo.transform == target.transform)
                return true;
        }
        return false;
    }

    public bool CallSimpleShoot()
    {
        if (_isAllowedToShoot)
        {
            if (DouglasTarget != null)
                DouglasShooting(DouglasTarget);

            _shootingDelay = MANUAL_SHOOTING_DELAY;

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

    private Vector3 DirectionToEnemy(Transform target) => (target.position - transform.position).normalized;
}
