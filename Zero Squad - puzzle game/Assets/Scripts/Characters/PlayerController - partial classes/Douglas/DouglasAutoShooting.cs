using UnityEngine;

public class DouglasAutoShooting : MonoBehaviour
{
    private PlayerController _playerController;
    private const float _autoShootingDelay = 2.2f;
    private float _shootingTimer;
    
    [Range(3, 5)]
    [SerializeField] private float _turningSpeed;

    private DouglasShootingManager _douglasShootingManager;
    private Transform _douglasRef;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        Initialization();
        _shootingTimer = _autoShootingDelay;
    }

    private void FixedUpdate()
    {
        var nearestEnemy = EnemyPoolController.FindClosestEnemy(transform.position);
        
        if (nearestEnemy != null)
        {
            float distance = Vector3.Distance(_douglasRef.position, nearestEnemy.transform.position);

            if (distance <= 12)
            {
                if (!nearestEnemy.GetComponent<EnemyHealth>().IsKilled)
                {
                    if (IsHavingClearShoot(nearestEnemy.transform))
                    {
                        transform.GetComponent<Animator>().SetBool("_isShooting", false);

                        FaceTarget(nearestEnemy.transform);

                        _shootingTimer -= Time.deltaTime;

                        if (_shootingTimer <= 0)
                            DouglasShooting(nearestEnemy.transform);
                    }
                    else
                        transform.GetComponent<Animator>().SetBool("_isShooting", false);
                }
                else
                {
                    transform.GetComponent<Animator>().SetBool("_isShooting", false);
                    nearestEnemy.GetComponent<EnemyPoolController>().enabled = false;
                }
            }
        }
    }

    private void DouglasShooting(Transform nearestEnemy)
    {
        _shootingTimer = _autoShootingDelay;
        nearestEnemy.GetComponent<EnemyHealth>().HealthDecreaseViaBullet();
        transform.GetComponent<Animator>().SetBool("_isShooting", true);
    }

    private void OnEnable()
    {
        Initialization();
        _shootingTimer = _autoShootingDelay;
    }

    private void Initialization()
    {
        if (_douglasRef == null)
            _douglasRef = GameObject.Find(CharactersEnum.Douglas.ToString()).transform;

        if (_douglasShootingManager == null)
            _douglasShootingManager = transform.GetComponent<DouglasShootingManager>();
    }

    public void FaceTarget(Transform TargetDetected)
    {
        Vector3 _targetDirection = DirectionToEnemy(TargetDetected);
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0, _targetDirection.z));
        _douglasRef.transform.rotation = Quaternion.Slerp(_douglasRef.transform.rotation, _lookRotation, Time.deltaTime * _turningSpeed);
    }

    private bool IsHavingClearShoot(Transform target)
    {
        //Debug.DrawRay(_douglasRef.position + (Vector3.up * 1.2f), DirectionToEnemy(target) * 12f, Color.red);

        RaycastHit hitInfo;
        if (Physics.Raycast(_douglasRef.position + (Vector3.up * 1.2f), DirectionToEnemy(target), out hitInfo, 12f, ~_playerController.AvoidLayersMasks))
        {
            if (hitInfo.collider.transform == target.transform)
                return true;
        }
        return false;
    }

    private Vector3 DirectionToEnemy(Transform target) => (target.position - _douglasRef.position).normalized;
}