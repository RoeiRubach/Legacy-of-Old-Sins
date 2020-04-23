using UnityEngine;

public class DouglasAutoShooting : MonoBehaviour
{
    private const float _autoShootingDelay = 2.2f;
    private float _shootingTimer;

    float timer = 2;
    [Range(3, 5)]
    [SerializeField] private float _turningSpeed;

    private DouglasShootingManager _douglasShootingManager;
    private Transform _douglasRef;

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

            if (distance <= 15)
            {
                transform.GetComponent<Animator>().SetBool("_isShooting", false);
                FaceTarget(nearestEnemy.transform);

                _shootingTimer -= Time.deltaTime;

                if (_shootingTimer <= 0)
                {
                    DouglasShooting(nearestEnemy.transform);
                }
            }
        }
    }

    private void DouglasShooting(Transform nearestEnemy)
    {
        _shootingTimer = _autoShootingDelay;

        if (!nearestEnemy.GetComponent<EnemyHealth>().CheckIfEnemyDead())
        {
            nearestEnemy.GetComponent<EnemyHealth>().HealthDecreaseViaBullet();
            transform.GetComponent<Animator>().SetBool("_isShooting", true);
        }
        else
            nearestEnemy.GetComponent<EnemyPoolController>().enabled = false;
    }

    private void OnEnable()
    {
        Initialization();
        _shootingTimer = _autoShootingDelay;
    }

    private void Initialization()
    {
        if (_douglasRef == null)
            _douglasRef = GameObject.FindGameObjectWithTag(CharactersEnum.Douglas.ToString()).transform;

        if (_douglasShootingManager == null)
            _douglasShootingManager = transform.GetComponent<DouglasShootingManager>();
    }

    public void FaceTarget(Transform TargetDetected)
    {
        Vector3 _targetDirection = (TargetDetected.position - _douglasRef.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0, _targetDirection.z));
        _douglasRef.transform.rotation = Quaternion.Slerp(_douglasRef.transform.rotation, _lookRotation, Time.deltaTime * _turningSpeed);
    }
}
