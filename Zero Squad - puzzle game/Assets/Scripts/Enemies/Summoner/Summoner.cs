using UnityEngine;

public class Summoner : EnemyBase, IElenaInteractables, IElenaAssassin
{
    [Range(5f, 20f)]
    [SerializeField] private float _spawnTimer = 5;
    [Range(5, 20)]
    [SerializeField] private int _zombieSpawnLimit = 5;
    [SerializeField] private GameObject _mindlessPossessedPrefabRef;
    [SerializeField] private GameObject _ShooterPrefabRef;
    [SerializeField] private bool _isShooterSummoning;
    [SerializeField] private Transform[] _shooterPlacements;

    private int _zombieSpawnCounter;
    private Vector3 _spawnLocation;
    private float _summonTimer;

    private void Start()
    {
        if (_isShooterSummoning)
        {
            _zombieSpawnLimit = 2;
            _zombieSpawnCounter = _zombieSpawnLimit;
        }

        _summonTimer = 0;
        _spawnLocation = transform.GetChild(0).position;
        _elenaKillSummonerPlacement = transform.GetChild(1);
    }
    
    private void Update()
    {
        if(_zombieSpawnCounter < _zombieSpawnLimit)
            _summonTimer -= Time.deltaTime;

        if (_summonTimer <= 0)
            SpawnEnemy();
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

    private void SpawnEnemy()
    {
        _summonTimer = _spawnTimer;

        if (_isShooterSummoning)
            ShooterSpawning();
        else
            MindlessPossessSpawning();

        _zombieSpawnCounter++;
    }

    private void MindlessPossessSpawning()
    {
        var mindless = Instantiate(_mindlessPossessedPrefabRef, _spawnLocation, Quaternion.identity);
        mindless.GetComponent<GameEventSubscriber>().AddListenerMethod(SpawnCountDecreasement);
        mindless.GetComponent<MindlessPossessed>().IsPlayerSpotted = true;
    }

    private void ShooterSpawning()
    {
        var shooter = Instantiate(_ShooterPrefabRef, _spawnLocation, Quaternion.identity);

        //shooter.GetComponent<GameEventSubscriber>().AddListenerMethod(SpawnCountDecreasement);
        //shooter.GetComponent<MindlessPossessed>().IsPlayerSpotted = true;
    }
}