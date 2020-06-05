using UnityEngine;

public class GameManager : SingletonDontDestroy<GameManager>
{
    public bool IsReachedCheckPoint;
    public Vector3[] CharactersPlacements;
}
