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
    [SerializeField] protected float _turningSpeed = 3f;
    [Space]

    [Header("Enemy's vision:", order = 1)]
    [SerializeField] protected Vector3 _enemyEyes = new Vector3(0, 1.6f, 0);
    [SerializeField] protected Vector3 _visionScale;
    [SerializeField] private Mesh _enemyConeVision;
    [Space]

    protected GameObject _enemyEyesRef;
    protected NavMeshAgent _enemyMeshAgent;
    protected Animator _enemyAnimator;
    protected bool _isEnemyRoaming;

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(_enemyConeVision, transform.position + _enemyEyes, transform.rotation, Vector3.Scale(_visionScale, new Vector3(1, 1, 1)));
    }

    protected void SetEnemyEyesAsInspector()
    {
        EnemyVisionRequirement._enemyEyesRef.transform.localPosition = _enemyEyes;
        EnemyVisionRequirement._enemyEyesRef.transform.localScale = _visionScale;
    }
}
