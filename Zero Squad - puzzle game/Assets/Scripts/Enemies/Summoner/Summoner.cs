using UnityEngine;

public class Summoner : EnemyBase, IElenaInteractables
{
    [Range(5f, 20f)]
    [SerializeField] private float _spawnTime;
    [SerializeField] private GameObject _mindlessPossessedPrefabRef;

    private Vector3 _spawnLocation;
    private Vector3 _elenaKillSummonerPlacement;

    private float _summonTimer;

    private void Start()
    {
        _summonTimer = 0;
        _spawnLocation = transform.GetChild(0).position;
        _elenaKillSummonerPlacement = transform.GetChild(1).position;
    }
    
    private void Update()
    {
        _summonTimer -= Time.deltaTime;

        if (_summonTimer <= 0)
        {
            _summonTimer = _spawnTime;
            var mindless = Instantiate(_mindlessPossessedPrefabRef, _spawnLocation, Quaternion.identity);

            mindless.GetComponent<MindlessPossessed>().IsPlayerSpotted = true;
        }
    }

    public void DestroyThisComponentViaEvent()
    {
        Destroy(GetComponent<Summoner>());
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
        return _elenaKillSummonerPlacement;
    }
}
