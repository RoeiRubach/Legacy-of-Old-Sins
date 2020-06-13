using UnityEngine;

public class TriggerBomb : MonoBehaviour
{
    [Range(1, 5)]
    [SerializeField] private int _bombTimer = 3;
    [Range(3, 5)]
    [SerializeField] private int _bombDamage = 3;
    private PlayerController _playerController;
    private Transform _charactersHolder;
    private Transform[] _charactersRef;
    private Door _doorRef;
    private BombSpawner _bombSpawner;
    
    private float _bombExplotionRadius = 3.25f;

    private void Awake()
    {
        _playerController = GameObject.Find("Characters Manager").GetComponent<PlayerController>();
        _charactersHolder = GameObject.Find("Characters").transform;
        _charactersRef = new Transform[_charactersHolder.childCount];
        for (int i = 0; i < _charactersHolder.childCount; i++)
        {
            _charactersRef[i] = _charactersHolder.GetChild(i);
        }
        _doorRef = GameObject.Find("BombTheDoor").GetComponent<Door>();
        _bombSpawner = FindObjectOfType<BombSpawner>();
    }

    public void TriggerBombInSeconds()
    {
        Invoke("DamageAnyCloseEntity", _bombTimer);
        Destroy(gameObject, _bombTimer);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _bombExplotionRadius);
    }
#endif

    private void DamageAnyCloseEntity()
    {
        GetComponent<GameEventSubscriber>()?.OnEventFire();
        for (int i = 0; i < _charactersRef.Length; i++)
        {
            float distance = GetDistanceToTarget(_charactersRef[i].position);

            if (IsCloseEnoughToDamage(distance))
                _playerController.DamageACharacter(_charactersRef[i], _bombDamage);
        }
        OpenDoorIfCloseEnough();
    }

    private void OpenDoorIfCloseEnough()
    {
        float distance = GetDistanceToTarget(_doorRef.transform.position);

        if (IsCloseEnoughToDamage(distance))
        {
            _doorRef.Invoke();
            Destroy(_bombSpawner.gameObject);
        }
        else
            _bombSpawner.SpawnABomb();
    }

    private bool IsCloseEnoughToDamage(float distance)
    {
        if (distance <= _bombExplotionRadius)
            return true;
        return false;
    }

    private float GetDistanceToTarget(Vector3 targetPosition) => Vector3.Distance(targetPosition, transform.position);
}
