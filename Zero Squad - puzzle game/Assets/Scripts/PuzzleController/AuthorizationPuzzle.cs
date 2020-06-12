using UnityEngine;

public class AuthorizationPuzzle : MonoBehaviour
{
    private const int _maxAuthorityCount = 3;
    private const int _minAuthorityCount = 0;
    private int _roomAuthorityCounter;
    private bool _isAll3RoomsBeenAuthorized;
    
    public void AddRoomAuthority()
    {
        if (_roomAuthorityCounter < _maxAuthorityCount)
        {
            _roomAuthorityCounter++;

            if(_roomAuthorityCounter == _maxAuthorityCount)
            {
                _isAll3RoomsBeenAuthorized = true;
                GetComponent<GameEventSubscriber>()?.OnEventFire();
            }
        }
    }
    
    public void RemoveRoomAuthority()
    {
        if (_isAll3RoomsBeenAuthorized)
        {
            _isAll3RoomsBeenAuthorized = false;
            GetComponent<GameEventSubscriber>()?.OnEventFire();
        }
        if (_roomAuthorityCounter > _minAuthorityCount) _roomAuthorityCounter--;
    }
}