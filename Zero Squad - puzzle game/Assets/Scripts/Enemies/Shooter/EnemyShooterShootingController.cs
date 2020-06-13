using UnityEngine;

public class EnemyShooterShootingController : MonoBehaviour
{
    [Range(3, 5)]
    [SerializeField] private float _turningSpeed = 3f;
    [Range(1, 5)]
    [SerializeField] private int _bulletDamageAmount = 1;
    [Range(2f, 5f)]
    [SerializeField] private float _autoShootingDelay = 3.3f;
    [SerializeField] private LayerMask _avoidLayerMask;
    [SerializeField] private Transform _bulletPrefab;
    [SerializeField] private Transform _gunEndPointPosition;

    private float _shootingTimer;

    private PlayerController _playerControllerHealth;
    private CharactersPoolController _nearestEnemy;
    private void Start()
    {
        _shootingTimer = _autoShootingDelay;
        _playerControllerHealth = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        _nearestEnemy = CharactersPoolController.FindClosestCharacter(transform.position);

        if (_nearestEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, _nearestEnemy.transform.position);

            if (distance <= 12)
            {
                if (IsHavingClearShoot(_nearestEnemy.transform))
                {
                    FaceTarget(_nearestEnemy.transform);

                    if ((_shootingTimer -= Time.deltaTime) <= 0)
                        SetShootAnimationTrue();
                }
            }
        }
    }

    private void SetShootAnimationTrue()
    {
        GetComponent<Animator>().SetBool(EnemyAnimationTransitionParameters._isAbleToAttack.ToString(), true);
    }

    public void OnAnimationShoot()
    {
        GetComponent<Animator>().SetBool(EnemyAnimationTransitionParameters._isAbleToAttack.ToString(), false);
        Transform bulletTransform = Instantiate(_bulletPrefab, _gunEndPointPosition.position, Quaternion.identity);
        bulletTransform.GetComponent<EnemyBullet>().SetUp(DirectionToEnemy(_nearestEnemy.transform));

        _shootingTimer = _autoShootingDelay;
    }

    private void OnEnable()
    {
        _shootingTimer = _autoShootingDelay;
    }

    public void FaceTarget(Transform TargetDetected)
    {
        Vector3 _targetDirection = DirectionToEnemy(TargetDetected);
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0, _targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _turningSpeed);
    }

    private bool IsHavingClearShoot(Transform target)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 1.2f), DirectionToEnemy(target), out hitInfo, 12f, ~_avoidLayerMask))
        {
            if (hitInfo.collider.transform == target.transform)
                return true;
        }
        return false;
    }

    private Vector3 DirectionToEnemy(Transform target) => (target.position - transform.position).normalized;
}