using UnityEngine;
using UnityEngine.Events;

public class GameEventSubscriber : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private UnityEvent _unityEvent;

    public void OnEventFire()
    {
        _unityEvent?.Invoke();
    }

    private void OnEnable()
    {
        _gameEvent += this;
    }

    private void OnDisable()
    {
        _gameEvent -= this;
    }
}
