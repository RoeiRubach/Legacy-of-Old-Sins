using UnityEngine;
using UnityEngine.AI;

public enum EnemyTransitionParameters
{
    _isMoving,
    _isPlayerBeenSeen,
    _isAbleToAttack
}

public enum EnemyDestinations
{
    _startPosition,
    _firstDestination,
    _secondDestination
}

[RequireComponent(typeof(EnemyVisionRequirement))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Enemy's attributes:", order = 0)]
    [SerializeField] protected int _enemyHP;
    [SerializeField] protected float _walkingSpeed = 0.3f;
    [SerializeField] protected float _runningSpeed = 1.25f;
    [SerializeField] protected float _turningSpeed = 2f;

    protected GameObject _enemyEyesRef;
    protected NavMeshAgent _enemyMeshAgent;
    protected Animator _enemyAnimator;
    protected bool _isEnemyRoaming;
}
