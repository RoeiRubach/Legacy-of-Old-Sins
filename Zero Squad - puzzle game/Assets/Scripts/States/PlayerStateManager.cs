using UnityEngine;
using UnityEngine.AI;

public abstract class PlayerStateManager
{
    protected PlayerController playerController;
    protected CameraController cameraController;

    public PlayerStateManager(PlayerController character, CameraController camera)
    {
        this.playerController = character;
        this.cameraController = camera;
    }

    #region Game state properties

    protected GameObject myCurrentCharacter;
    protected NavMeshAgent myCurrentAgent;

    #endregion

    #region Game state methods 

    public abstract void Handle();

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public virtual void PointAndClickMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit myRacycastHit;
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out myRacycastHit, Mathf.Infinity, playerController.walkableLayerMask))
                myCurrentAgent.SetDestination(myRacycastHit.point);
        }
    }
#endregion
}