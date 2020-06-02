using UnityEngine;

public class Shooter : EnemyBase, IDouglasEnemies, IElenaAssassin, IElenaInteractables
{ 
    public Vector3 CharacterInteractionPlacement()
    {
        return _elenaKillSummonerPlacement.position;
    }

    public void Interact()
    {
    }
}
