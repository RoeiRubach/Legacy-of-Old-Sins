using UnityEngine;

public class Summoner : EnemyBase, IElenaAssassin
{
    [Range(5f, 20f)]
    [SerializeField] private float _spawnTimer = 5;
    [Range(1, 20)]
    [SerializeField] private int _zombieSpawnLimit = 3;
    [SerializeField] private GameObject _mindlessPossessedPrefabRef;
    [SerializeField] private GameObject _ShooterPrefabRef;
    [SerializeField] private bool _isShooterSummoning;
    [SerializeField] private Transform[] _shooterPlacements;

    private Transform _shooterSpawnPlacement;
    private int _zombieSpawnCounter;
    private Vector3 _spawnLocation;
    private float _summonTimer;

    private void Start()
    {
        transform.name = "Summoner";
        _summonTimer = _spawnTimer;
        _spawnLocation = transform.GetChild(0).position;
        _elenaKillSummonerPlacement = transform.GetChild(1);

        if (_isShooterSummoning)
        {
            _zombieSpawnLimit = 2;
            _zombieSpawnCounter = _zombieSpawnLimit;
            //_summonTimer = _spawnTimer;
        }
    }
    
    private void Update()
    {
        if (_zombieSpawnCounter >= _zombieSpawnLimit) return;

        if ((_summonTimer -= Time.deltaTime) <= 0)
            SpawnEnemy();
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
        ElenaState.IsAssassinatingTarget = false;
    }

    public Vector3 CharacterInteractionPlacement() => _elenaKillSummonerPlacement.position;

    public void MindlessSpawnCountDecreasement()
    {
        if (_zombieSpawnCounter == 0) return;
        _zombieSpawnCounter--;
    }

    public void SetMindlessSummoningViaEvent()
    {
        if (_zombieSpawnCounter == _zombieSpawnLimit) return;
        _zombieSpawnCounter++;
    }

    public void ShooterSpawnCountDecreasement()
    {
        _zombieSpawnCounter--;
        for(int i = 0; i < _shooterPlacements.Length; i++)
        {
            if(_shooterPlacements[i].childCount == 0)
            {
                _shooterSpawnPlacement = _shooterPlacements[i];
                break;
            }
        }
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
        mindless.GetComponent<GameEventSubscriber>().AddListenerMethod(MindlessSpawnCountDecreasement);
        mindless.GetComponent<MindlessPossessed>().IsPlayerSpotted = true;
    }

    private void ShooterSpawning()
    {
        var shooter = Instantiate(_ShooterPrefabRef, _spawnLocation, Quaternion.identity, _shooterSpawnPlacement);
        shooter.GetComponent<GameEventSubscriber>().AddListenerMethod(ShooterSpawnCountDecreasement);
        shooter.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_shooterSpawnPlacement.position);
    }
}