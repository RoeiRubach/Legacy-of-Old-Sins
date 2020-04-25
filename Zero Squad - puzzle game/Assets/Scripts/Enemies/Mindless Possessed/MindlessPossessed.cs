﻿using UnityEngine;
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
        {
            ResetAIPath();
            SetDeathAnimationTrue();
            Destroy(gameObject, destroyTimer);
        }
    }

    private void RoamingState(float distanceToDestination)
    {
        if (_isPlayerEscape)
            ResetEnemyRoaming();

        SetPropertiesRoamState();

        if (distanceToDestination <= enemyMeshAgent.stoppingDistance + 1f)
        {
            isEnemyRoaming = false;
            ResetAIPath();
        }

        RoamingDestinationController();
    }

    private void AttackingState(float distanceToDestination)
    {
        if (distanceToDestination <= enemyMeshAgent.stoppingDistance)
        {
            IsAttacking = true;
            enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isMoving.ToString(), false);
            enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isAbleToAttack.ToString(), true);
            ResetAIPath();

            FaceTarget(TargetDetected);
        }
        else
        {
            IsAttacking = false;
            enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isAbleToAttack.ToString(), false);
            _isPlayerEscape = true;
        }
    }

    private void ResetEnemyRoaming()
    {
        _isPlayerEscape = false;
        isEnemyRoaming = true;
        ResetAIPath();
        _enemyDestinations = EnemyDestinations._firstDestination;
        _destinationToGoTo = _startPosition;
    }

    private void RoamingDestinationController()
    {
        if (!enemyMeshAgent.hasPath)
        {
            if (!isEnemyRoaming)
            {
                enemyMeshAgent.stoppingDistance = _stoppingDistanceNoTarget;
                enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isMoving.ToString(), false);

                if (_firstDestination != null && _secondDestination != null)
                {
                    switch (_enemyDestinations)
                    {
                        case EnemyDestinations._startPosition:
                            isEnemyRoaming = true;
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
        }
        else
            enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isMoving.ToString(), true);
    }

    private void SetPropertiesRoamState()
    {
        enemyMeshAgent.speed = _walkingSpeed;
        enemyMeshAgent.stoppingDistance = 0f;
        enemyMeshAgent.SetDestination(_destinationToGoTo);

        enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isPlayerBeenSeen.ToString(), false);
        enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isAbleToAttack.ToString(), false);
    }

    private void SetPropertiesChaseState()
    {
        enemyMeshAgent.speed = _runningSpeed;
        enemyMeshAgent.stoppingDistance = 1.3f;
        _destinationToGoTo = TargetDetected.position;
        enemyMeshAgent.SetDestination(_destinationToGoTo);

        enemyAnimator.SetBool(EnemyAnimationTransitionParameters._isPlayerBeenSeen.ToString(), true);
    }

    private IEnumerator SetNewDestination(Vector3 newDestination, EnemyDestinations enemyDestinations)
    {
        isEnemyRoaming = true;
        yield return new WaitForSeconds(_waitSeconds);
        _destinationToGoTo = newDestination;
        _enemyDestinations = enemyDestinations;
    }
}