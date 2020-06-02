using UnityEngine;

public class EnemyShooterShootingController : MonoBehaviour
{
    [Range(3, 5)]
    [SerializeField] private float _turningSpeed = 3f;
    [Range(1, 5)]
    [SerializeField] private int _bulletDamageAmount = 1;
    [Range(2f, 5f)]
    [SerializeField] private float _autoShootingDelay = 2.2f;
    [SerializeField] private LayerMask _avoidLayerMask;

    private float _shootingTimer;

    private PlayerController _playerControllerHealth;

    private void Start()
    {
        _shootingTimer = _autoShootingDelay;
        _playerControllerHealth = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        var nearestEnemy = CharactersPoolController.FindClosestEnemy(transform.position);

        if (nearestEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, nearestEnemy.transform.position);

            if (distance <= 12)
            {
                if (IsHavingClearShoot(nearestEnemy.transform))
                {
                    //transform.GetComponent<Animator>().SetBool("_isShooting", false);

                    FaceTarget(nearestEnemy.transform);

                    _shootingTimer -= Time.deltaTime;

                    if (_shootingTimer <= 0)
                        Shoot(nearestEnemy.transform);
                }
                //if (!nearestEnemy.GetComponent<EnemyHealth>().CheckIfEnemyDead)
                //{
                //    else
                //        transform.GetComponent<Animator>().SetBool("_isShooting", false);
                //}
                //else
                //{
                //    transform.GetComponent<Animator>().SetBool("_isShooting", false);
                //    nearestEnemy.GetComponent<EnemyPoolController>().enabled = false;
                //}
            }
        }
    }

    private void Shoot(Transform nearestEnemy)
    {
        _shootingTimer = _autoShootingDelay;

        for(int i = 0; i < _bulletDamageAmount; i++)
        {
            switch (nearestEnemy.tag)
            {
                case "Douglas":
                    _playerControllerHealth.DouglasTakingDamage();
                    break;
                case "Elena":
                    _playerControllerHealth.ElenaTakingDamage();
                    break;
                case "Hector":
                    _playerControllerHealth.HectorTakingDamage();
                    break;
            }
        }
        ////transform.GetComponent<Animator>().SetBool("_isShooting", true);
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
        //Debug.DrawRay(_douglasRef.position + (Vector3.up * 1.2f), DirectionToEnemy(target) * 12f, Color.red);

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