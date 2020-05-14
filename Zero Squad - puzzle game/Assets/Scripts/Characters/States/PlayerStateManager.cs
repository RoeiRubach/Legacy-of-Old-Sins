using UnityEngine;
using UnityEngine.AI;

public enum CharactersAnimationTransitionParameters
{
    _isMoving,
    _isSkillMode,
    _isLifting,
    _isCarrying,
}

public abstract class PlayerStateManager
{
    protected int runningSpeed = 5, walkingSpeed = 2;

    protected PlayerController playerController;
    protected CameraController cameraController;

    protected Transform interactableObject;

    protected bool isPossibleToInteract, isInteracting, isCabinetInteracting;

    protected PlayerStateManager(PlayerController character)
    {
        playerController = character;
    }
    protected PlayerStateManager(PlayerController character, CameraController camera)
    {
        playerController = character;
        cameraController = camera;
    }

    #region Game state properties

    protected GameObject myCurrentCharacter;
    protected NavMeshAgent myCurrentAgent;
    protected Animator myCurrentAnimator;
    
    protected bool _initializationComplete;
    public bool EnterSkillViaButton;

    #endregion

    #region Game state abstract methods

    public abstract void UpdateHandle();
    public abstract void OnStateEnter();
    public abstract void OnTriggerEnter(string tagReceived, HealthRegenCollectables healthRegenCollectables);
    public abstract void OnStateExit();
    public abstract void EnterOrExitSkillMode();

    #endregion

    #region Game state virtual methods

    protected virtual void PointAndClickMovement()
    {
        myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isSkillMode.ToString(), false);

        if (Input.GetMouseButtonDown(1))
        {
            if (!isCabinetInteracting)
            {
                RaycastHit hitInfo;

                // Generate a ray from the cursor position
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, playerController.walkableLayerMask))
                {
                    if (isPossibleToInteract)
                    {
                        isInteracting = true;
                        myCurrentAgent.SetDestination(CharacterInteractionPlacement());
                    }
                    else
                    {
                        isInteracting = false;
                        ResetInteractable();
                        myCurrentAgent.SetDestination(hitInfo.point);
                    }

                    if (playerController.IsLifting)
                        myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isCarrying.ToString(), true);
                    else
                        myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isMoving.ToString(), true);
                }
            }
        }

        else if (!myCurrentAgent.hasPath)
        {
            if (isInteracting)
                CharacterObjectInteraction();

            if (playerController.IsLifting)
                myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isCarrying.ToString(), false);
            else
                myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isMoving.ToString(), false);
        }
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
    protected virtual void TurnTowardTheCursor()
    {
        myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isSkillMode.ToString(), true);

        ResetAIPath();

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
        }
    }
    #endregion

    protected void ResetInteractable()
    {
        if (interactableObject != null && !isInteracting)
        {
            interactableObject.GetComponent<Outline>().enabled = false;
            interactableObject = null;
        }
    }

    protected void ResetAIPath()
    {
        if (myCurrentAgent.hasPath)
        {
            myCurrentAgent.isStopped = true;
            myCurrentAgent.ResetPath();
        }
    }

    private Vector3 CharacterInteractionPlacement()
    {
        return interactableObject.GetComponent<IInteractable>().CharacterInteractionPlacement();
    }

    protected virtual void CharacterObjectInteraction()
    {
        if (interactableObject.GetComponent<IInteractable>() != null)
        {
            float dis = Vector3.Distance(myCurrentCharacter.transform.position, CharacterInteractionPlacement());

            if (dis <= 2.3f)
            {
                if (interactableObject.name == "Bomb")
                {
                    myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isLifting.ToString(), true);
                    myCurrentAgent.speed = walkingSpeed;
                }
                else if (interactableObject.name == "Cabinet")
                    isCabinetInteracting = true;

                else if (interactableObject.name == "Health Pack")
                    HealthPackInteraction();

                interactableObject.GetComponent<IInteractable>().Interact();

                myCurrentAgent.SetDestination(CharacterInteractionPlacement());
            }
            else
            {
                isInteracting = false;
                ResetInteractable();
            }
        }
        else
        {
            isCabinetInteracting = false;
            isInteracting = false;
            interactableObject = null;
        }
    }

    private void HealthPackInteraction()
    {
        var healthRegenCollectables = interactableObject.GetComponent<HealthRegenCollectables>();
        Debug.Log(healthRegenCollectables.HealthToRegen);

        switch (myCurrentCharacter.tag)
        {
            case "Douglas":
                playerController.DouglasGainingHealth(healthRegenCollectables.HealthToRegen);
                break;
            case "Elena":
                playerController.ElenaGainingHealth(healthRegenCollectables.HealthToRegen);
                break;
            case "Hector":
                playerController.HectorGainingHealth(healthRegenCollectables.HealthToRegen);
                break;
        }

        isInteracting = false;
        healthRegenCollectables.CallOnDestroy();
    }
}