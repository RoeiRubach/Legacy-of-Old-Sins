using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MindlessPossessed : EnemyBase
{
    EnemyDestinations _enemyDestinations;
    private float _waitSeconds = 3f, _stoppingDistanceNoTarget, _stoppingDistanceTargeted = 2f;
    private bool _isPlayerEscape;
    private Vector3 _destinationToGoTo, _startPosition;
    
    [SerializeField] private Transform _firstDestination, _secondDestination;

    private void Start()
    {
        _startPosition = transform.position;
        _enemyMeshAgent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsEnemyGotKilled())
        {
            float _distanceToDestination = Vector3.Distance(_destinationToGoTo, transform.position);

            if (!IsPlayerSpotted)
                RoamingState(_distanceToDestination);

            else
            {
                SetPropertiesChaseState();
                AttackingState(_distanceToDestination);
            }
        }
        else
            Destroy(gameObject, _destroyTimer);
    }

    private void RoamingState(float distanceToDestination)
    {
        if (_isPlayerEscape)
            ResetEnemyRoaming();

        SetPropertiesRoamState();

        if (distanceToDestination <= _enemyMeshAgent.stoppingDistance + 1f)
        {
            _isEnemyRoaming = false;
            ResetAIPath();
        }

        RoamingDestinationController();
    }

    private void AttackingState(float distanceToDestination)
    {
        if (distanceToDestination <= _enemyMeshAgent.stoppingDistance)
        {
            _enemyAnimator.SetBool(EnemyTransitionParameters._isMoving.ToString(), false);
            _enemyAnimator.SetBool(EnemyTransitionParameters._isAbleToAttack.ToString(), true);
            ResetAIPath();

            FaceTarget(TargetDetected);
        }
        else
        {
            _enemyAnimator.SetBool(EnemyTransitionParameters._isAbleToAttack.ToString(), false);
            _isPlayerEscape = true;
        }
    }

    private void ResetEnemyRoaming()
    {
        _isPlayerEscape = false;
        _isEnemyRoaming = true;
        ResetAIPath();
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
                        FaceTarget(_secondDestination);
                        break;

                    case EnemyDestinations._secondDestination:
                        StartCoroutine(SetNewDestination(_firstDestination.position, EnemyDestinations._firstDestination));
                        FaceTarget(_firstDestination);
                        break;
                }
            }
        }
        else
            _enemyAnimator.SetBool(EnemyTransitionParameters._isMoving.ToString(), true);
    }

    private void SetPropertiesRoamState()
    {
        _enemyMeshAgent.speed = _walkingSpeed;
        _enemyMeshAgent.stoppingDistance = 0f;
        _enemyMeshAgent.SetDestination(_destinationToGoTo);

        _enemyAnimator.SetBool(EnemyTransitionParameters._isPlayerBeenSeen.ToString(), false);
        _enemyAnimator.SetBool(EnemyTransitionParameters._isAbleToAttack.ToString(), false);
    }

    private void SetPropertiesChaseState()
    {
        _enemyMeshAgent.speed = _runningSpeed;
        _enemyMeshAgent.stoppingDistance = 1.3f;
        _destinationToGoTo = TargetDetected.position;
        _enemyMeshAgent.SetDestination(_destinationToGoTo);

        _enemyAnimator.SetBool(EnemyTransitionParameters._isPlayerBeenSeen.ToString(), true);
    }

    private IEnumerator SetNewDestination(Vector3 newDestination, EnemyDestinations enemyDestinations)
    {
        _isEnemyRoaming = true;
        yield return new WaitForSeconds(_waitSeconds);
        _destinationToGoTo = newDestination;
        _enemyDestinations = enemyDestinations;
    }
}
