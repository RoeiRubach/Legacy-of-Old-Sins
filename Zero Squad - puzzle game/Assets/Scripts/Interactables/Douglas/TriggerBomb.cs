using UnityEngine;

public class TriggerBomb : MonoBehaviour
{
    [Range(1, 5)]
    [SerializeField] private int _bombTimer = 3;
    [Range(3, 5)]
    [SerializeField] private int _bombDamage = 3;
    [SerializeField] private GameObject _explosionPrefab;
    private PlayerController _playerController;
    private Transform _charactersHolder;
    private Transform[] _charactersRef;
    private Door _doorRef;
    private BombSpawner _bombSpawner;
    private Transform _finalCheckPoint;
    private Animator _bombAnimator;
    
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
        _finalCheckPoint = GameObject.Find("FinalCheckPoint").transform;
        _bombSpawner = FindObjectOfType<BombSpawner>();
        _bombAnimator = GetComponent<Animator>();
    }

    public void TriggerBombInSeconds()
    {
        Invoke("TriggerBombExplosion", _bombTimer);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _bombExplotionRadius);
    }
#endif

    private void TriggerBombExplosion()
    {
        if (GetComponent<Animator>())
            _bombAnimator.SetTrigger("BombExplode");
        else
        {
            BombExplosion();
            Destroy(gameObject, 0.5f);
        }
    }

    public void BombExplosion()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        DamageAnyCloseEntity();
        Destroy(GetComponentInParent<Bomb>());
    }

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
            _finalCheckPoint.GetComponent<BoxCollider>().enabled = true;
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
