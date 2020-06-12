using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    private const string _elenaName = "Elena";
    private PuzzleController _puzzleController;
    private AuthorizationPuzzle _authorizationPuzzle;
    private Transform _authorizedCharacter;
    private static int _mindlessInRoomsCounter;
    private bool _isMindlessNotAloneInRoom;
    private bool _isMindlessInRoom;
    private bool _isRoomBeenAuthorized;

    private void Awake()
    {
        _puzzleController = GetComponentInParent<PuzzleController>();
        _authorizationPuzzle = GetComponentInParent<AuthorizationPuzzle>();
    }

    #region On Trigger methods
    private void OnTriggerEnter(Collider other)
    {
        if (!IsEnteredCharacterHasAuthorization(other.transform)) return;

        if (!transform.GetChild(0).gameObject.activeSelf)
            RegisterAuthorizationRoom(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isRoomBeenAuthorized) return;
        if (!_isMindlessInRoom) return;
        else
        {
            if (!IsEnteredCharacterHasAuthorization(other.transform)) return;

            PuzzleController.IsPuzzleComplite = false;
            if (other.transform != _authorizedCharacter)
            {
                _isMindlessNotAloneInRoom = true;
                return;
            }
            else
            {
                if (_mindlessInRoomsCounter == 3)
                    if (!_isMindlessNotAloneInRoom)
                        PuzzleController.IsPuzzleComplite = true;
            }
        }

        if (_authorizedCharacter == null)
        {
            PuzzleController.IsPuzzleComplite = false;
            GetComponent<GameEventSubscriber>()?.OnEventFire();
            _isMindlessInRoom = false;
            _mindlessInRoomsCounter--;
            DeregisterAuthorizationRoom();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isRoomBeenAuthorized && _authorizedCharacter == null) return;

        if (other.transform == _authorizedCharacter)
            DeregisterAuthorizationRoom(_authorizedCharacter);
        else
        {
            if (IsEnteredCharacterHasAuthorization(other.transform))
                _isMindlessNotAloneInRoom = false;
        }
    }
    #endregion

    public void CheckIfZombieInRoomViaEvent()
    {
        if (!_isRoomBeenAuthorized) return;

        if (_mindlessInRoomsCounter != 3)
            GetComponent<GameEventSubscriber>()?.OnEventFire();
        else
        {
            if (PuzzleController.IsPuzzleComplite)
            {
                _puzzleController.PuzzleComplition();
            }
        }
    }

    private void RegisterAuthorizationRoom(Transform enteredCharacter)
    {
        _isRoomBeenAuthorized = true;
        _authorizedCharacter = enteredCharacter;
        if (enteredCharacter.GetComponent<MindlessPossessed>())
        {
            _isMindlessInRoom = true;
            _mindlessInRoomsCounter++;
        }

        transform.GetChild(0).gameObject.SetActive(true);
        _authorizationPuzzle.AddRoomAuthority();
    }

    private void DeregisterAuthorizationRoom(Transform enteredCharacter = null)
    {
        PuzzleController.IsPuzzleComplite = false;
        _isRoomBeenAuthorized = false;
        if(enteredCharacter != null)
        {
            if (enteredCharacter == _authorizedCharacter)
                _authorizedCharacter = null;
            if (enteredCharacter.GetComponent<MindlessPossessed>())
            {
                _mindlessInRoomsCounter--;
                _isMindlessInRoom = false;
            }
        }

        transform.GetChild(0).gameObject.SetActive(false);
        _authorizationPuzzle.RemoveRoomAuthority();
    }

    private bool IsEnteredCharacterHasAuthorization(Transform enteredCharacter)
    {
        var hasAuthorization = enteredCharacter.GetComponent<IPuzzleAuthority>();
        if (hasAuthorization != null && !enteredCharacter.CompareTag(_elenaName))
            return true;
        else
            return false;
    }
}
