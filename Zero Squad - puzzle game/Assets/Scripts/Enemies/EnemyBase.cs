using UnityEngine;
using UnityEngine.AI;

public enum EnemyTransitionParameter
{
    _isMoving,
    _isPlayerBeenSeen,
    _isAbleToAttack
}

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected int _enemyHP;
    protected NavMeshAgent _enemyMeshAgent;
    protected Animator _enemyAnimator;

    [SerializeField] protected float _lookRadius = 10f;

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }
}
