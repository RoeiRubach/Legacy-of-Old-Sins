using UnityEngine;

public class Summoner : EnemyBase, IElenaInteractables, IElenaAssassin
{
    [Range(5f, 20f)]
    [SerializeField] private float _spawnTime;
    [SerializeField] private GameObject _mindlessPossessedPrefabRef;

    [Range(5, 20)]
    [SerializeField] private int _zombieSpawnLimit;

    private int _zombieSpawnCounter;

    private Vector3 _spawnLocation;

    private float _summonTimer;

    private void Start()
    {
        _summonTimer = 0;
        _spawnLocation = transform.GetChild(0).position;
        _elenaKillSummonerPlacement = transform.GetChild(1);
    }
    
    private void Update()
    {
        if(_zombieSpawnCounter < _zombieSpawnLimit)
        _summonTimer -= Time.deltaTime;

        if (_summonTimer <= 0)
        {
            _summonTimer = _spawnTime;
            var mindless = Instantiate(_mindlessPossessedPrefabRef, _spawnLocation, Quaternion.identity);
            mindless.GetComponent<GameEventSubscriber>().AddListenerMethod(SpawnCountDecreasement);
            mindless.GetComponent<MindlessPossessed>().IsPlayerSpotted = true;
            _zombieSpawnCounter++;
        }
    }

    public void SpawnCountDecreasement()
    {
        _zombieSpawnCounter--;
    }

    public void DestroyThisComponentViaEvent()
    {
        Destroy(GetComponent<Summoner>());
        Destroy(gameObject);
    }

    public void Interact()
    {
        if (!transform.GetComponentInChildren<EnemyTargetDetecting>().IsElenaBeenSpotted)
        {
            GetComponent<GameEventSubscriber>()?.OnEventFire();
            Destroy(gameObject);
        }
    }

    public Vector3 CharacterInteractionPlacement()
    {
        return _elenaKillSummonerPlacement.position;
    }
}
