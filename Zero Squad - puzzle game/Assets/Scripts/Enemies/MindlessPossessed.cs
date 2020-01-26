using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MindlessPossessed : EnemyBase
{
    EnemyDestinations _enemyDestinations;
    private float _waitSeconds = 3f, _stoppingDistanceNoTarget, _stoppingDistanceTargeted = 2f;

    [HideInInspector] public bool IsPlayerSpotted;
    private bool _isPlayerEscape;
    private Vector3 _destinationToGoTo, _startPosition;
    
    [SerializeField] private Transform _firstDestination, _secondDestination;

    [HideInInspector] public Transform TargetDetected;

    private void Start()
    {
        _startPosition = transform.position;
        _enemyMeshAgent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        float _distanceToDestination = Vector3.Distance(_destinationToGoTo, transform.position);

        if (!IsPlayerSpotted)
        {
            if (_isPlayerEscape)
                ResetEnemyRoaming();

            SetPropertiesToRoamMode();

            if (_distanceToDestination <= _enemyMeshAgent.stoppingDistance + 1f)
            {
                _isEnemyRoaming = false;
                _enemyMeshAgent.isStopped = true;
                _enemyMeshAgent.ResetPath();
            }

            RoamingDestinationController();
        }
        else
        {
            SetPropertiesToAttackMode();

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
                _isPlayerEscape = true;
            }
        }
    }

    private void ResetEnemyRoaming()
    {
        _isPlayerEscape = false;
        _isEnemyRoaming = true;
        _enemyMeshAgent.isStopped = true;
        _enemyMeshAgent.ResetPath();
        _enemyDestinations = EnemyDestinations._firstDestination;
        _destinationToGoTo = _startPosition;
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
                        StartCoroutine(SetNewDestination(_firstDestination.position, EnemyDestinations._firstDestination));
                        break;
                }
            }
        }
        else
            _enemyAnimator.SetBool(EnemyTransitionParameters._isMoving.ToString(), true);
    }

    private void SetPropertiesToRoamMode()
    {
        _enemyMeshAgent.speed = _walkingSpeed;
        _enemyMeshAgent.stoppingDistance = 0f;
        _enemyMeshAgent.SetDestination(_destinationToGoTo);

        _enemyAnimator.SetBool(EnemyTransitionParameters._isPlayerBeenSeen.ToString(), false);
        _enemyAnimator.SetBool(EnemyTransitionParameters._isAbleToAttack.ToString(), false);
    }

    private void SetPropertiesToAttackMode()
    {
        _enemyMeshAgent.speed = _runningSpeed;
        _enemyMeshAgent.stoppingDistance = 1.3f;
        _destinationToGoTo = TargetDetected.position;
        _enemyMeshAgent.SetDestination(_destinationToGoTo);

        _enemyAnimator.SetBool(EnemyTransitionParameters._isMoving.ToString(), false);
        _enemyAnimator.SetBool(EnemyTransitionParameters._isPlayerBeenSeen.ToString(), true);
    }

    private void FaceTarget()
    {
        Vector3 _targetDirection = (TargetDetected.position - transform.position).normalized;
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
