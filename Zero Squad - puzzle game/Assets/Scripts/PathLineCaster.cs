using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathLineCaster : MonoBehaviour
{
    private NavMeshAgent agentToLinePath;

    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject flag3D;

    private GameObject flagClone;

    private Vector3 destinationClone;

    private void Start()
    {
        agentToLinePath = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (agentToLinePath.hasPath)
        {
            lineRenderer.positionCount = agentToLinePath.path.corners.Length;
            lineRenderer.SetPositions(agentToLinePath.path.corners);
            lineRenderer.enabled = true;

            FlagInstantiateManager();
        }
        else
        {
            lineRenderer.enabled = false;

            DestroyFlagCloneIfNotNull();
        }
    }

    private void FlagInstantiateManager()
    {
        if ((destinationClone != agentToLinePath.destination))
        {
            DestroyFlagCloneIfNotNull();

            destinationClone = agentToLinePath.destination;
            flagClone = Instantiate(flag3D, agentToLinePath.destination, flag3D.transform.rotation);
        }
    }

    private void DestroyFlagCloneIfNotNull()
    {
        if (flagClone != null)
            Destroy(flagClone);
    }
}
