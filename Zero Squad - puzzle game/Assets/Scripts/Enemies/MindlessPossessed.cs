using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MindlessPossessed : EnemyBase
{
    private float _waitSeconds = 3f;
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
        if (!_enemyMeshAgent.hasPath)
        {
            _enemyAnimator.SetBool(EnemyTransitionParameter._isMoving.ToString(), false);

            if (transform.position == _startPosition)
                StartCoroutine(SetEnemyDestination(_firstDestination.position));

            else if (transform.position == _firstDestination.position)
                StartCoroutine(SetEnemyDestination(_secondDestination.position));

            else if (transform.position == _secondDestination.position)
                StartCoroutine(SetEnemyDestination(_startPosition));
        }
        else
            _enemyAnimator.SetBool(EnemyTransitionParameter._isMoving.ToString(), true);
    }

    private IEnumerator SetEnemyDestination(Vector3 _newDestination)
    {
        yield return new WaitForSeconds(_waitSeconds);
        _enemyMeshAgent.SetDestination(_newDestination);
    }
}
