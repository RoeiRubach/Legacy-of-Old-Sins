using UnityEngine;

public class GameManager : SingletonDontDestroy<GameManager>
{
    public bool IsReachedCheckPoint;
    [HideInInspector] public bool IsReachedFinalCheckPoint;
    [HideInInspector] public Vector3[] CharactersPlacements;

#if UNITY_EDITOR
    [Range(1, 3)]
    [SerializeField] private int _checkPointToSpawn = 1;

    [SerializeField] private Transform[] _firstCheckPoint;
    [SerializeField] private Transform[] _secondCheckPoint;
    [SerializeField] private Transform[] _thirdCheckPoint;
    [SerializeField] private GameObject[] _checkPoints;

    protected override void Awake()
    {
        base.Awake();

        if (IsReachedCheckPoint)
            switch (_checkPointToSpawn)
            {
                case 1:
                    _checkPoints[0].SetActive(true);
                    SetCheckPointsLocation(_firstCheckPoint);
                    break;
                case 2:
                    _checkPoints[1].SetActive(true);
                    SetCheckPointsLocation(_secondCheckPoint);
                    break;
                case 3:
                    _checkPoints[2].SetActive(true);
                    IsReachedFinalCheckPoint = true;
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
#endif
}
