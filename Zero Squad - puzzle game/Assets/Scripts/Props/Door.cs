using UnityEngine;

public class Door : MonoBehaviour
{
    private const float _maxDownwardsMovement = -2.299f;
    private const float _maxUpwardsMovement = 1.429f;
    private bool _isInvoke;
    private bool _isDownwards = false;

    private void FixedUpdate()
    {
        if (!_isInvoke) return;

        DoorMovementHandler();
    }

    public void Invoke()
    {
        _isInvoke = true;
        _isDownwards = !_isDownwards ? true : false;
    }

    //private void InvokeIsInvokedOFF()
    //{
    //    _isInvoke = false;
    //    _isDownwards = !_isDownwards ? true : false;
    //}

    private void MoveDoorDownwards()
    {
        if (transform.localPosition.y <= _maxDownwardsMovement)
        {
            _isInvoke = false;
            return;
        } 
        transform.position += Vector3.down * Time.deltaTime;
    }

    private void MoveDoorUpwards()
    {
        if (transform.position.y >= _maxUpwardsMovement)
        {
            GetComponent<GameEventSubscriber>()?.OnEventFire();
            _isInvoke = false;
            return;
        }
        transform.position += Vector3.up * Time.deltaTime;
    }

    private void DoorMovementHandler()
    {
        if (_isDownwards)
            MoveDoorDownwards();
        else
            MoveDoorUpwards();
    }
}
