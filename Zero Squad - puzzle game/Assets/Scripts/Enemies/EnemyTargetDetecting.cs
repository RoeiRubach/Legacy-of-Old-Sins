using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetDetecting : MonoBehaviour
{
    private string _douglasName = "Douglas", _elenaName = "Elena";
    private string _hectorName = "Hector", _hectorShieldName = "Hector Shield";

    private float _delayTime = 0.85f;
    
    //private bool _isDouglasBeenSpotted, _isElenaBeenSpotted, _isHectorBeenSpotted;
    private bool _isElenaBeenSpotted, _isHectorShieldSpotted;

    [SerializeField] Transform _hectorShieldRef, _enemiesShieldHittingSpot;

    private MindlessPossessed _mindlessPossessedRef;
    private ElenaStealthManager _elenaStealthManager;

    private Transform _douglasRef, _hectorRef, _elenaRef;

    private NavMeshHit _navMeshHit;

    private void Start()
    {
        _mindlessPossessedRef = GetComponentInParent<MindlessPossessed>();
        _douglasRef = GameObject.FindWithTag("Douglas").transform;
        _hectorRef = GameObject.FindWithTag("Hector").transform;
        _elenaRef = GameObject.FindWithTag("Elena").transform;
    }

    private void Update()
    {
        var nearestCharacter = CharactersPoolController.FindClosestEnemy(transform.position);

        if (nearestCharacter != null)
        {
            if (nearestCharacter.transform.CompareTag(_elenaName) && !_isElenaBeenSpotted)
                return;

            _mindlessPossessedRef.IsPlayerSpotted = true;

            if (!_mindlessPossessedRef.IsAttacking)
                _mindlessPossessedRef.TargetDetected = nearestCharacter.transform;
            else
            {
                if (nearestCharacter.transform != _enemiesShieldHittingSpot)
                    _mindlessPossessedRef.TargetDetected = nearestCharacter.transform;
            }
        }
        else
            _mindlessPossessedRef.IsPlayerSpotted = false;
    }

    #region On Trigger States
    /// <summary>
    /// Checking if Elena entered enemy's sight.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(_elenaName))
            ElenaEnterDetected(other);
    }

    /// <summary>
    /// Checking if Elena still in enemy's sight and if she stayed/left enemy's area.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag(_elenaName))
            ElenaEnterDetected(other);
    }

    /// <summary>
    /// Checking if the character left enemy's sight.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (_isElenaBeenSpotted)
        {
            if (other.CompareTag(_elenaName))
                ElenaOutOfSight();
        }
    }
    #endregion

    private void ElenaEnterDetected(Collider other)
    {
        if (IsCharacterOnDetectedArea(other))
        {
            Debug.DrawRay(_mindlessPossessedRef.transform.position + (Vector3.up * 1.2f), DirectionToElena(other.transform) * 7.6f, Color.red);

            RaycastHit hitInfo;
            if (Physics.Raycast(_mindlessPossessedRef.transform.position + (Vector3.up * 1.2f), DirectionToElena(other.transform), out hitInfo, 7.6f))
            {
                if (hitInfo.transform.CompareTag(_elenaName))
                    ElenaBeenSpotted(hitInfo.collider);
            }
        }
    }

    private void ElenaBeenSpotted(Collider _elena)
    {
        if (_elenaStealthManager == null)
            _elenaStealthManager = _elena.GetComponentInParent<ElenaStealthManager>();

        _mindlessPossessedRef.TargetDetected = _elena.transform;
        _isElenaBeenSpotted = true;
        _elenaStealthManager.AddElenaToPool();
        Debug.Log("Elena got triggered");
    }

    private void ElenaOutOfSight()
    {
        _isElenaBeenSpotted = false;
        _elenaStealthManager.RemoveElenaFromPool();
        Debug.Log("Elena got out of trigger");
    }

    private bool IsCharacterOnDetectedArea(Collider other)
    {
        return !NavMesh.SamplePosition(other.transform.position, out _navMeshHit, 0.1f,
                                            NavMesh.GetAreaFromName("Mindless possessed"));
    }

    private Vector3 DirectionToElena(Transform elenaRef) => (elenaRef.position - _mindlessPossessedRef.transform.position).normalized;

}
