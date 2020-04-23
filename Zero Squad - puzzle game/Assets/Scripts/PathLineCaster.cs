using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathLineCaster : MonoBehaviour
{
    private NavMeshAgent _agentToLinePath;

    private LineRenderer _lineRenderer;

    [SerializeField] private GameObject _flag3D;

    private GameObject _flagClone;

    private Vector3 _destinationClone;

    private void Start()
    {
        _agentToLinePath = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_agentToLinePath.hasPath)
        {
            _lineRenderer.positionCount = _agentToLinePath.path.corners.Length;
            _lineRenderer.SetPositions(_agentToLinePath.path.corners);
            _lineRenderer.enabled = true;

            FlagInstantiateController();
        }
        else
        {
            _lineRenderer.enabled = false;

            DestroyFlagCloneIfNotNull();
        }
    }

    private void FlagInstantiateController()
    {
        if ((_destinationClone != _agentToLinePath.destination))
        {
            DestroyFlagCloneIfNotNull();

            _destinationClone = _agentToLinePath.destination;
            _flagClone = Instantiate(_flag3D, _agentToLinePath.destination, _flag3D.transform.rotation);
        }
    }

    private void DestroyFlagCloneIfNotNull()
    {
        if (_flagClone != null)
        {
            Destroy(_flagClone);

            if (!GetComponent<LineRenderer>().enabled)
                GetComponent<Animator>().SetBool(CharactersAnimationTransitionParameters._isMoving.ToString(), false);
        }
    }

}
