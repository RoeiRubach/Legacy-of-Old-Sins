using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent myAgent;

    [SerializeField]
    private Camera myCamera;

    [SerializeField]
    private LayerMask walkableLayerMask;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit myRacycastHit;
            Ray myRay = myCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out myRacycastHit, Mathf.Infinity, walkableLayerMask))
                myAgent.SetDestination(myRacycastHit.point);
        }
    }
}
