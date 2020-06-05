using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonDontDestroy<GameManager>
{
    public bool IsReachedCheckPoint;
    public Vector3[] CharactersPlacements;

    private void Start()
    {
    }
}
