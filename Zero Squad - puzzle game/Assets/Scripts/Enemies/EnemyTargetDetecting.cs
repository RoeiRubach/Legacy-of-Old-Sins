using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetDetecting : MonoBehaviour
{
    private string _douglasName = "Douglas";
    private string _elenaName = "Elena";
    private string _hectorName = "Hector";

    private float _delayTime = 1f;

    private bool _isDouglasBeenSeen, _isElenaBeenSeen, _isHectorBeenSeen;

    private MindlessPossessed _mindlessPossessedRef;

    private NavMeshHit _navMeshHit;

    private void Start()
    {
        _mindlessPossessedRef = GetComponentInParent<MindlessPossessed>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_mindlessPossessedRef.IsPlayerSpotted)
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_mindlessPossessedRef.IsPlayerSpotted)
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
        else
        {
            if (other.CompareTag(_douglasName))
            {
                if (!IsCharacterOnDetectedArea(other))
                {
                    _mindlessPossessedRef.IsPlayerSpotted = false;
                    DouglasOutOfSight();
                }
            }
            else if (other.CompareTag(_elenaName))
            {
                if (!IsCharacterOnDetectedArea(other))
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isDouglasBeenSeen)
        {
            if (other.CompareTag(_douglasName))
                DouglasOutOfSight();
        }
        else if (_isElenaBeenSeen)
        {
            if (other.CompareTag(_elenaName))
                ElenaOutOfSight();
        }
        else if (_isHectorBeenSeen)
        {
            if (other.CompareTag(_hectorName))
                HectorOutOfSight();
        }
    }

    private void DouglasBeenSpotted(Collider _douglas)
    {
        _mindlessPossessedRef.TargetDetected = _douglas.transform;
        _isDouglasBeenSeen = true;
        _mindlessPossessedRef.IsPlayerSpotted = true;
        Debug.Log("Douglas got triggered");
    }

    private void ElenaBeenSpotted(Collider _elena)
    {
        _mindlessPossessedRef.TargetDetected = _elena.transform;
        _isElenaBeenSeen = true;
        _mindlessPossessedRef.IsPlayerSpotted = true;
        Debug.Log("Elena got triggered");
    }

    private void HectorBeenSpotted(Collider _hector)
    {
        _mindlessPossessedRef.TargetDetected = _hector.transform;
        _isHectorBeenSeen = true;
        _mindlessPossessedRef.IsPlayerSpotted = true;
        Debug.Log("Hector got triggered");
    }

    private void DouglasOutOfSight()
    {
        _isDouglasBeenSeen = false;
        Invoke("SetOffPlayerSpottedWithDelay", _delayTime);
        Debug.Log("Douglas got out of trigger");
    }

    private void ElenaOutOfSight()
    {
        _isElenaBeenSeen = false;
        Invoke("SetOffPlayerSpottedWithDelay", _delayTime);
        Debug.Log("Elena got out of trigger");
    }

    private void HectorOutOfSight()
    {
        _isHectorBeenSeen = false;
        Invoke("SetOffPlayerSpottedWithDelay", _delayTime);
        Debug.Log("Hector got out of trigger");
    }

    private void SetOffPlayerSpottedWithDelay()
    {
        _mindlessPossessedRef.TargetDetected = null;
        _mindlessPossessedRef.IsPlayerSpotted = false;
    }

    private bool IsCharacterOnDetectedArea(Collider other)
    {
        return !NavMesh.SamplePosition(other.transform.position, out _navMeshHit, 0.1f, NavMesh.GetAreaFromName("Mindless possessed"));
    }
}
