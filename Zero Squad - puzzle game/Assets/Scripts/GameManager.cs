using UnityEngine;

public class GameManager : SingletonDontDestroy<GameManager>
{
    public bool IsReachedCheckPoint;
    [HideInInspector] public Vector3[] CharactersPlacements;
    [Range(1,3)]
    [SerializeField] private int _checkPointToSpawn = 1;

    [SerializeField] private Transform[] _firstCheckPoint;
    [SerializeField] private Transform[] _secondCheckPoint;
    [SerializeField] private Transform[] _thirdCheckPoint;

    protected override void Awake()
    {
        base.Awake();

        if(IsReachedCheckPoint)
            switch (_checkPointToSpawn)
            {
                case 1:
                    SetCheckPointsLocation(_firstCheckPoint);
                    break;
                case 2:
                    SetCheckPointsLocation(_secondCheckPoint);
                    break;
                case 3:
                    SetCheckPointsLocation(_thirdCheckPoint);
                    break;
            }
    }

    private void SetCheckPointsLocation(Transform[] placements)
    {
        for (int i = 0; i < CharactersPlacements.Length; i++)
        {
            CharactersPlacements[i] = placements[i].position;
        }
    }
}
