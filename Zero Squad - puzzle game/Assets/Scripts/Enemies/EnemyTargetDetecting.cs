using UnityEngine;

public class EnemyTargetDetecting : MonoBehaviour
{
    private string _douglasName = "Douglas";
    private string _elenaName = "Elena";
    private string _hectorName = "Hector";

    private bool _isDouglasBeenSeen, _isElenaBeenSeen, _isHectorBeenSeen;

    private MindlessPossessed mindlessPossessedRef;

    private void Start()
    {
        mindlessPossessedRef = GetComponentInParent<MindlessPossessed>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!mindlessPossessedRef._isPlayerSpotted)
        {
            if (other.CompareTag(_douglasName))
            {
                mindlessPossessedRef._targetDetected = other.transform;
                _isDouglasBeenSeen = true;
                mindlessPossessedRef._isPlayerSpotted = true;
                Debug.Log("Douglas got triggered");
            }
            else if (other.CompareTag(_elenaName))
            {
                mindlessPossessedRef._targetDetected = other.transform;
                _isElenaBeenSeen = true;
                mindlessPossessedRef._isPlayerSpotted = true;
                Debug.Log("Elena got triggered");
            }
            else if (other.CompareTag(_hectorName))
            {
                mindlessPossessedRef._targetDetected = other.transform;
                _isHectorBeenSeen = true;
                mindlessPossessedRef._isPlayerSpotted = true;
                Debug.Log("Hector got triggered");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!mindlessPossessedRef._isPlayerSpotted)
        {
            if (other.CompareTag(_douglasName))
            {
                mindlessPossessedRef._targetDetected = other.transform;
                _isDouglasBeenSeen = true;
                mindlessPossessedRef._isPlayerSpotted = true;
                Debug.Log("Douglas got triggered on stay");
            }
            else if (other.CompareTag(_elenaName))
            {
                mindlessPossessedRef._targetDetected = other.transform;
                _isElenaBeenSeen = true;
                mindlessPossessedRef._isPlayerSpotted = true;
                Debug.Log("Elena got triggered on stay");
            }
            else if (other.CompareTag(_hectorName))
            {
                mindlessPossessedRef._targetDetected = other.transform;
                _isHectorBeenSeen = true;
                mindlessPossessedRef._isPlayerSpotted = true;
                Debug.Log("Hector got triggered on stay");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isDouglasBeenSeen)
        {
            if (other.CompareTag(_douglasName))
            {
                _isDouglasBeenSeen = false;
                Invoke("SetPlayerSpottedWithDelay", 3f);
                Debug.Log("Douglas got out of trigger");
            }
        }
        else if (_isElenaBeenSeen)
        {
            if (other.CompareTag(_elenaName))
            {
                _isElenaBeenSeen = false;
                Invoke("SetPlayerSpottedWithDelay", 3f);
                Debug.Log("Elena got out of trigger");
            }
        }
        else if (_isHectorBeenSeen)
        {
            if (other.CompareTag(_hectorName))
            {
                _isHectorBeenSeen = false;
                Invoke("SetPlayerSpottedWithDelay", 3f);
                Debug.Log("Hector got out of trigger");
            }
        }
    }

    private void SetPlayerSpottedWithDelay()
    {
        mindlessPossessedRef._targetDetected = null;
        mindlessPossessedRef._isPlayerSpotted = false;
    }
}
