using UnityEngine;

public class EnemyTargetDetecting : MonoBehaviour
{
    public bool IsElenaBeenSpotted { get; private set; }
    public ElenaStealthManager ElenaStealthManager { get; private set; }
    [SerializeField] private Transform _enemiesShieldHittingSpot;

    private float _delayTime = 0.85f;
    private bool _isHectorShieldSpotted;
    private EnemyBase _enemyBaseRef;

    private void Start()
    {
        _enemyBaseRef = GetComponentInParent<EnemyBase>();
        _enemiesShieldHittingSpot = _enemiesShieldHittingSpot.GetChild(0).GetChild(0);
    }

    private void Update()
    {
        var nearestCharacter = CharactersPoolController.FindClosestCharacter(transform.position, this, ElenaStealthManager);

        if (nearestCharacter == null) return;

        if (_enemyBaseRef.IsPlayerSpotted)
        {
            SetElenaOffSightIfInStealth();
            SetTargetIfNotAttackingAlready(nearestCharacter.transform);
        }
        else
        {
            _enemyBaseRef.TargetDetected = null;
            _enemyBaseRef.IsPlayerSpotted = false;

            if (ElenaStealthManager != null && IsElenaBeenSpotted)
                InvokeElenaOutOfSight();
        }
    }

    #region On Trigger States
    /// <summary>
    /// Checking if Elena entered enemy's sight.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(CharactersEnum.Elena.ToString()))
            ElenaEnterDetected(other);
    }

    /// <summary>
    /// Checking if Elena still in enemy's sight and if she stayed/left enemy's area.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag(CharactersEnum.Elena.ToString()) && !IsElenaBeenSpotted)
            ElenaEnterDetected(other);
    }

    /// <summary>
    /// Checking if the character left enemy's sight.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (IsElenaBeenSpotted)
        {
            if (other.CompareTag(CharactersEnum.Elena.ToString()))
                InvokeElenaOutOfSight();
        }
    }
    #endregion

    #region Elena's methods
    private void ElenaEnterDetected(Collider other)
    {
        if (GetComponentInParent<Summoner>()) return;

        RaycastHit hitInfo;
        if (Physics.Raycast(_enemyBaseRef.transform.position + (Vector3.up * 1.2f), DirectionToElena(other.transform), out hitInfo, 7.6f))
        {
            if (hitInfo.transform.CompareTag(CharactersEnum.Elena.ToString()))
                ElenaBeenSpotted(hitInfo.collider);
        }
    }

    private void ElenaBeenSpotted(Collider elena)
    {
        if (ElenaStealthManager == null)
            ElenaStealthManager = elena.GetComponentInParent<ElenaStealthManager>();

        CancelInvoke("ElenaOutOfSight");
        _enemyBaseRef.MindlessShooterSFX.PlayRandomDetecedElenaClip();
        _enemyBaseRef.TargetDetected = elena.transform;
        IsElenaBeenSpotted = true;

        ElenaStealthManager.AddElenaToPool();
    }

    private void InvokeElenaOutOfSight()
    {
        if (!IsInvoking("ElenaOutOfSight"))
            Invoke("ElenaOutOfSight", 1.1f);
    }

    private void ElenaOutOfSight()
    {
        IsElenaBeenSpotted = false;
        _enemyBaseRef.MindlessShooterSFX.PlayRandomLostElenaClip();
        ElenaStealthManager.RemoveElenaFromPool();
    }

    private void SetElenaOffSightIfInStealth()
    {
        if (ElenaStealthManager != null)
            if (ElenaStealthManager.IsInStealthMode && IsElenaBeenSpotted)
                InvokeElenaOutOfSight();
    }

    private Vector3 DirectionToElena(Transform elenaRef) => (elenaRef.position - _enemyBaseRef.transform.position).normalized;
    #endregion

    private void SetTargetIfNotAttackingAlready(Transform nearestCharacter)
    {
        if (!_enemyBaseRef.IsAttacking)
        {
            if(_enemyBaseRef.TargetDetected != nearestCharacter)
            {
                _enemyBaseRef.MindlessShooterSFX.PlayRandomCharacterTriggerClip();
                _enemyBaseRef.TargetDetected = nearestCharacter;
            }
        }
        else
        {
            if (nearestCharacter != _enemiesShieldHittingSpot)
                _enemyBaseRef.TargetDetected = nearestCharacter;
        }
    }
}
