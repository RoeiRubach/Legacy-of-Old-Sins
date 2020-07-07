using UnityEngine;

public class Shooter : EnemyBase, IDouglasEnemies, IElenaAssassin
{
    private EnemyShooterShootingController _shootingController;

    private void Start()
    {
        MindlessShooterSFX = GetComponent<MindlessShooterSFX>();
        transform.name = "Shooter - root";
        _elenaKillSummonerPlacement = transform.GetChild(1);
        _shootingController = GetComponentInChildren<EnemyShooterShootingController>();
        _shootingController.enabled = false;
        IsPlayerSpotted = true;
    }

    private void Update()
    {
        if (!IsEnemyGotKilled())
        {
            if(!enemyMeshAgent.hasPath)
                if(IsPlayerSpotted)
                    if (!_shootingController.enabled)
                    {
                        _shootingController.enabled = true;
                        IsAttacking = true;
                    }
        }
        else
        {
            ResetAIPath();
            SetDeathAnimationTrue();
            Destroy(gameObject, destroyTimer);
        }
    }

    public Vector3 CharacterInteractionPlacement() => _elenaKillSummonerPlacement.position;

    public void Interact()
    {
        if (!transform.GetComponentInChildren<EnemyTargetDetecting>().IsElenaBeenSpotted)
            enemyHealth.AssassinateEnemy();

        ElenaState.IsAssassinatingTarget = false;
    }
}
