using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MindlessPossessed : EnemyBase
{
    EnemyDestinations _enemyDestinations;
    private float _waitSeconds = 3f, _stoppingDistanceNoTarget, _stoppingDistanceTargeted = 2f;
    [HideInInspector]
    public bool _isPlayerSpotted, _isAttacking;
    private Vector3 _destinationToGoTo, _startPosition;

    [Space]
    [SerializeField] private Transform _firstDestination, _secondDestination;

    [HideInInspector]
    public Transform _targetDetected;

    private void Start()
    {
        _startPosition = transform.position;
        _enemyMeshAgent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<Animator>();
        SetEnemyEyesAsInspector();
    }

    private void Update()
    {
        float _distanceToDestination = Vector3.Distance(_destinationToGoTo, transform.position);

        if (!_isPlayerSpotted)
        {
            _enemyMeshAgent.speed = _walkingSpeed;
            _enemyMeshAgent.SetDestination(_destinationToGoTo);

            _enemyAnimator.SetBool(EnemyTransitionParameters._isPlayerBeenSeen.ToString(), false);
            _enemyAnimator.SetBool(EnemyTransitionParameters._isAbleToAttack.ToString(), false);
            _enemyMeshAgent.stoppingDistance = 0f;

            if (_distanceToDestination <= _enemyMeshAgent.stoppingDistance + 2f)
            {
                _isEnemyRoaming = false;
                _enemyMeshAgent.isStopped = true;
                _enemyMeshAgent.ResetPath();
            }

            RoamingDestinationController();
        }
        else
        {
            _enemyMeshAgent.speed = _runningSpeed;
            _enemyMeshAgent.stoppingDistance = 1.3f;
            _destinationToGoTo = _targetDetected.position;
            _enemyMeshAgent.SetDestination(_destinationToGoTo);
            _enemyAnimator.SetBool(EnemyTransitionParameters._isMoving.ToString(), false);
            _enemyAnimator.SetBool(EnemyTransitionParameters._isPlayerBeenSeen.ToString(), true);


            if (_distanceToDestination <= _enemyMeshAgent.stoppingDistance)
            {
                _enemyAnimator.SetBool(EnemyTransitionParameters._isAbleToAttack.ToString(), true);
                _enemyMeshAgent.isStopped = true;
                _enemyMeshAgent.ResetPath();

                FaceTarget();
            }
            else
            {
                _enemyAnimator.SetBool(EnemyTransitionParameters._isAbleToAttack.ToString(), false);
            }

        }
    }

    private void RoamingDestinationController()
    {
        if (!_enemyMeshAgent.hasPath)
        {
            if (!_isEnemyRoaming)
            {
                _enemyMeshAgent.stoppingDistance = _stoppingDistanceNoTarget;
                _enemyAnimator.SetBool(EnemyTransitionParameters._isMoving.ToString(), false);

                switch (_enemyDestinations)
                { 
                    case EnemyDestinations._startPosition:
                        _isEnemyRoaming = true;
                        _enemyDestinations = EnemyDestinations._firstDestination;
                        _destinationToGoTo = _firstDestination.position;
                        break;

                    case EnemyDestinations._firstDestination:
                        StartCoroutine(SetNewDestination(_secondDestination.position, EnemyDestinations._secondDestination));
                        break;

                    case EnemyDestinations._secondDestination:
                        StartCoroutine(SetNewDestination(_startPosition, EnemyDestinations._firstDestination));
                        break;
                }
            }
        }
        else
            _enemyAnimator.SetBool(EnemyTransitionParameters._isMoving.ToString(), true);
    }

    private void FaceTarget()
    {
        Vector3 _targetDirection = (_targetDetected.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0, _targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _turningSpeed);
    }

    private IEnumerator SetNewDestination(Vector3 newDestination, EnemyDestinations enemyDestinations)
    {
        _isEnemyRoaming = true;
        yield return new WaitForSeconds(_waitSeconds);
        _destinationToGoTo = newDestination;
        _enemyDestinations = enemyDestinations;
    }
}
