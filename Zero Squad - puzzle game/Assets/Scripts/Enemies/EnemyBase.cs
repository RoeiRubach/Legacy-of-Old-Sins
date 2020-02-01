using UnityEngine;
using UnityEngine.AI;

public enum EnemyTransitionParameters
{
    _isMoving,
    _isPlayerBeenSeen,
    _isAbleToAttack,
    _isDead
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
    [HideInInspector] public Transform TargetDetected;
    [HideInInspector] public bool IsPlayerSpotted;

    [Header("Enemy's attributes:", order = 0)]
    [SerializeField] protected int _enemyHP;
    [SerializeField] protected float _walkingSpeed, _runningSpeed, _turningSpeed;

    protected GameObject _enemyEyesRef;
    protected NavMeshAgent _enemyMeshAgent;
    protected Animator _enemyAnimator;
    protected bool _isEnemyRoaming;
    protected float _destroyTimer = 4f;

    private string _bullet = "Bullet";

#if UNITY_EDITOR
    [ContextMenu("Kill enemy - PLAYMODE ONLY!")]
    public void EnemyDamageTakingTest()
    {
        ResetAIPath();
        _enemyAnimator.SetBool(EnemyTransitionParameters._isDead.ToString(), true);
    }
#endif

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_bullet))
        {
            _enemyHP--;

            if (_enemyHP <= 0)
            {
                ResetAIPath();
                _enemyAnimator.SetBool(EnemyTransitionParameters._isDead.ToString(), true);
            }
        }
    }

    protected virtual bool IsEnemyGotKilled()
    {
        return _enemyAnimator.GetBool(EnemyTransitionParameters._isDead.ToString());
    }

    protected virtual void ResetAIPath()
    {
        _enemyMeshAgent.isStopped = true;
        _enemyMeshAgent.ResetPath();
    }

    protected virtual void FaceTarget(Transform TargetDetected)
    {
        Vector3 _targetDirection = (TargetDetected.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0, _targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _turningSpeed);
    }
}
