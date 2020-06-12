using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private Transform _lightsNewParent;
    private PuzzleHandler[] _placements;
    public static bool IsPuzzleComplite;

    private void Awake()
    {
        _placements = GetComponentsInChildren<PuzzleHandler>();
    }

    public void PuzzleComplition()
    {
        if (!IsPuzzleComplite) return;

        TransferLightsToNewParent();
        GetComponent<GameEventSubscriber>()?.OnEventFire();
    }

    private void TransferLightsToNewParent()
    {
        for (int i = 0; i < _placements.Length; i++)
        {
            _placements[i].transform.GetChild(0).parent = _lightsNewParent;
        }
    }
}
