using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMindlessPossessed : MonoBehaviour
{
    [SerializeField] private GameObject _mindlessPossessedPrefabRef;
    private Vector3 _spawnLocation;

    [Range(3.5f, 7f)]
    [SerializeField] private float _spawnTime;

    private float _summonTimer;

    private void Start()
    {
        _summonTimer = 0;
        _spawnLocation = transform.GetChild(0).position;
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
        Destroy(GetComponent<SummonMindlessPossessed>());
    }
}
