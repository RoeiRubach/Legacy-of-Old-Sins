using UnityEngine;
using UnityEngine.AI;

public enum TransitionParameter
{
    _isMoving,
}

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
    protected Animator animator;

    protected float invokeSpeedManager = 0.2f;
    protected bool initializationComplete;
    public bool enterSkillViaButton;

    #endregion

    #region Game state abstract methods

    public abstract void UpdateHandle();
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void EnterOrExitSkillMode();

    #endregion

    #region Game state virtual methods 

    public virtual void PointAndClickMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit myRacycastHit;

            // Generate a ray from the cursor position
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out myRacycastHit, Mathf.Infinity, playerController.walkableLayerMask))
            {
                myCurrentAgent.SetDestination(myRacycastHit.point);
                animator.SetBool(TransitionParameter._isMoving.ToString(), true);
            }
        }

        else if (!myCurrentAgent.hasPath)
            animator.SetBool(TransitionParameter._isMoving.ToString(), false);
    }

    /// <summary>
    /// Stops and reset the current character's destination (if it has one).
    /// Creates an "imaginary" plane and casting a ray on it with the cursor.
    /// 
    /// Determine the point where the cursor ray intersects the plane.
    /// This will be the point that the object must look towards to be looking at the mouse.
    /// Raycasting to a Plane object only gives me a distance, so I have to take the distance,
    /// then find the point along that ray that meets that distance. This will be the point to look at.
    /// </summary>
    public virtual void TurnTowardTheCursor()
    {
        if (myCurrentAgent.hasPath)
        {
            myCurrentAgent.isStopped = true;
            myCurrentAgent.ResetPath();
            animator.SetBool(TransitionParameter._isMoving.ToString(), false);
        }

        myCurrentAgent.enabled = false;

        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, myCurrentCharacter.transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;

        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation. This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - myCurrentCharacter.transform.position);

            myCurrentCharacter.transform.rotation = targetRotation;

            // Smoothly rotate towards the target point.
            //myCurrentCharacter.transform.rotation = Quaternion.Slerp(myCurrentCharacter.transform.rotation, targetRotation, 2.5f * Time.deltaTime);
        }
    }

    #endregion
}