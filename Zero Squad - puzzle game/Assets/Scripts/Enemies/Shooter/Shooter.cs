using UnityEngine;

public class Shooter : EnemyBase, IDouglasEnemies, IElenaAssassin, IElenaInteractables
{
    private EnemyShooterShootingController _shootingController;

    private void Start()
    {
        _elenaKillSummonerPlacement = transform.GetChild(1);
        _shootingController = GetComponentInChildren<EnemyShooterShootingController>();
        _shootingController.enabled = false;
    }

    private void Update()
    {
        if (!IsEnemyGotKilled())
        {
            if(!enemyMeshAgent.hasPath)
                if(TargetDetected != null)
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

    public Vector3 CharacterInteractionPlacement()
    {
        return _elenaKillSummonerPlacement.position;
    }

    public void Interact()
    {
        if (!transform.GetComponentInChildren<EnemyTargetDetecting>().IsElenaBeenSpotted)
        {
            enemyHealth.AssassinateEnemy();
        }
    }
}
