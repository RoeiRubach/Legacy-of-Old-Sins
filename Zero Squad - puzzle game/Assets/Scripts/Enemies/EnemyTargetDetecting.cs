using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetDetecting : MonoBehaviour
{
    private string _douglasName = "Douglas", _elenaName = "Elena";
    private string _hectorName = "Hector", _hectorShield = "Hector Shield";

    private float _delayTime = 1f;
    
    private bool _isDouglasBeenSpotted, _isElenaBeenSpotted, _isHectorBeenSpotted;

    private MindlessPossessed _mindlessPossessedRef;
    private ElenaStealthManager elenaStealthManager;

    private NavMeshHit _navMeshHit;

    private void Start()
    {
        _mindlessPossessedRef = GetComponentInParent<MindlessPossessed>();
    }

    /// <summary>
    /// Checking if the character entered enemy's sight.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        HectorShieldTriggerEnterController(other);

        if (!_mindlessPossessedRef.IsPlayerSpotted)
            CharactersEnterDetected(other);
    }

    /// <summary>
    /// Checking if the character still in enemy's sight and if it stayed/left the enemy's area.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        HectorShieldTriggerStayController(other);

        if (!_mindlessPossessedRef.IsPlayerSpotted)
            CharactersEnterDetected(other);

        else
            CharactersOutOfAreaController(other);
    }

    /// <summary>
    /// Checking if the character left the enemy's sight.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (_isDouglasBeenSpotted)
        {
            if (other.CompareTag(_douglasName))
                DouglasOutOfSight();
        }
        else if (_isElenaBeenSpotted)
        {
            if (other.CompareTag(_elenaName))
                ElenaOutOfSight();
        }
        else if (_isHectorBeenSpotted)
        {
            if (other.CompareTag(_hectorName))
                HectorOutOfSight();
        }
    }

    private void HectorShieldTriggerEnterController(Collider other)
    {
        if (other.CompareTag(_hectorShield) && !_mindlessPossessedRef.IsAttacking)
        {
            if (IsCharacterOnDetectedArea(other))
            {
                HectorShieldBeenSpotted(other.transform.GetChild(0));
                other.gameObject.GetComponentInParent<BoxCollider>().enabled = true;
            }
        }
    }

    private void HectorShieldTriggerStayController(Collider other)
    {
        if (other.CompareTag("Shield holder"))
        {
            if (other.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                _isDouglasBeenSpotted = false;
                _isElenaBeenSpotted = false;
                _isHectorBeenSpotted = false;
                return;
            }
            else
            {
                other.gameObject.GetComponentInParent<BoxCollider>().enabled = false;
                SetOffPlayerSpotted();
            }
        }
    }

    private void CharactersEnterDetected(Collider other)
    {
        if (other.CompareTag(_douglasName))
        {
            if (IsCharacterOnDetectedArea(other))
                DouglasBeenSpotted(other);
        }
        else if (other.CompareTag(_elenaName))
        {
            if (IsCharacterOnDetectedArea(other))
                ElenaBeenSpotted(other);
        }
        else if (other.CompareTag(_hectorName))
        {
            if (IsCharacterOnDetectedArea(other))
                HectorBeenSpotted(other);
        }
    }

    private void CharactersOutOfAreaController(Collider other)
    {
        if (other.CompareTag(_douglasName))
        {
            if (!IsCharacterOnDetectedArea(other))
            {
                _mindlessPossessedRef.IsPlayerSpotted = false;
                DouglasOutOfSight();
            }
        }
        else if (other.CompareTag(_elenaName) || elenaStealthManager.IsInStealthMode)
        {
            if (!IsCharacterOnDetectedArea(other) || elenaStealthManager.IsInStealthMode)
            {
                _mindlessPossessedRef.IsPlayerSpotted = false;
                ElenaOutOfSight();
            }
        }
        else if (other.CompareTag(_hectorName))
        {
            if (!IsCharacterOnDetectedArea(other))
            {
                _mindlessPossessedRef.IsPlayerSpotted = false;
                HectorOutOfSight();
            }
        }
    }

    private void DouglasBeenSpotted(Collider _douglas)
    {
        _mindlessPossessedRef.TargetDetected = _douglas.transform;
        _isDouglasBeenSpotted = true;
        _mindlessPossessedRef.IsPlayerSpotted = true;
        Debug.Log("Douglas got triggered");
    }

    private void ElenaBeenSpotted(Collider _elena)
    {
        elenaStealthManager = _elena.GetComponentInParent<ElenaStealthManager>();
        _mindlessPossessedRef.TargetDetected = _elena.transform;
        _isElenaBeenSpotted = true;
        _mindlessPossessedRef.IsPlayerSpotted = true;
        Debug.Log("Elena got triggered");
    }

    private void HectorBeenSpotted(Collider _hector)
    {
        _mindlessPossessedRef.TargetDetected = _hector.transform;
        _isHectorBeenSpotted = true;
        _mindlessPossessedRef.IsPlayerSpotted = true;
        Debug.Log("Hector got triggered");
    }

    private void HectorShieldBeenSpotted(Transform _hectorShield)
    {
        _mindlessPossessedRef.TargetDetected = _hectorShield;
        Debug.Log("Hector shield got triggered");
    }

    private void DouglasOutOfSight()
    {
        _isDouglasBeenSpotted = false;
        Invoke("SetOffPlayerSpotted", _delayTime);
        Debug.Log("Douglas got out of trigger");
    }

    private void ElenaOutOfSight()
    {
        _isElenaBeenSpotted = false;
        Invoke("SetOffPlayerSpotted", _delayTime);
        Debug.Log("Elena got out of trigger");
    }

    private void HectorOutOfSight()
    {
        _isHectorBeenSpotted = false;
        Invoke("SetOffPlayerSpotted", _delayTime);
        Debug.Log("Hector got out of trigger");
    }

    private void SetOffPlayerSpotted()
    {
        _mindlessPossessedRef.TargetDetected = null;
        _mindlessPossessedRef.IsPlayerSpotted = false;
    }

    private bool IsCharacterOnDetectedArea(Collider other)
    {
        return !NavMesh.SamplePosition(other.transform.position, out _navMeshHit, 0.1f,
                                            NavMesh.GetAreaFromName("Mindless possessed"));
    }
}
