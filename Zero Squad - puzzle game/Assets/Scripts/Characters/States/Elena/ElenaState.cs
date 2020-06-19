using UnityEngine;

public class ElenaState : PlayerStateManager
{
    private bool _isUsingSkill;
    private GameObject _elenaAgentPlacement;
    private ElenaStealthManager _elenaStealthManager;
    public static bool IsAbleToAssassinTarget, IsAssassinatingTarget;

    public ElenaState(PlayerController character) : base(character)
    {
    }
    public ElenaState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void UpdateHandle()
    {
        PointAndClickMovement();

        if (!_isUsingSkill)
            HighlightCursorOverInteractableObject();
        else
        {
            if (isPossibleToInteract)
                ResetAssassinating();
            myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isSkillMode.ToString(), true);
        }

        EnterOrExitSkillMode();
        SwitchCharacters();
    }

    protected override void PointAndClickMovement()
    {
        base.PointAndClickMovement();

        if (isInteracting)
        {
            if (IsAbleToAssassinTarget)
            {
                if (interactableObject.GetComponentInChildren<EnemyTargetDetecting>().IsElenaBeenSpotted)
                {
                    IsAbleToAssassinTarget = false;
                    IsAssassinatingTarget = false;
                    isInteracting = false;
                    ResetInteractable();
                    ResetAIPath();
                }
                else
                    IsAssassinatingTarget = true;
            }
        }
    }

    public override void EnterOrExitSkillMode()
    {
        if (initializationComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space) || EnterSkillViaButton)
            {
                EnterSkillViaButton = false;
                _isUsingSkill = !_isUsingSkill ? true : false;

                if (_isUsingSkill && !_elenaStealthManager.IsStealthOnCooldown)
                {
                    if (IsElenaAboutToBackstab())
                        ResetAssassinating();
                    ResetAIPath();
                    myCurrentAgent.speed = stealthRunningSpeed;
                    _elenaStealthManager.CallStealthMode();
                    playerController.ElenaOnSkillMode();
                }
                else
                {
                    _isUsingSkill = false;
                    myCurrentAgent.speed = runningSpeed;
                    _elenaStealthManager.OffStealthMode();
                    myCurrentAgent.enabled = true;
                    playerController.ElenaOffSkillMode();
                }
                if (GameManager.Instance.IsReachedFinalCheckPoint) return;
                if (TutorialPopUpsController.Instance.MyTutorialHandler["Stealth mode"])
                {
                    TutorialPopUpsController.Instance.DestroyFirstChild();
                    TutorialPopUpsController.Instance.DisplayFirstChild();
                }
            }
        }
    }

    public override void OnStateEnter()
    {
        ElenaInitialization();
        ShowPopupNeeded("Stealth mode");
        ShowPopupNeeded("Backstab");
    }

    public override void OnTriggerEnter(string tagReceived)
    {
        switch (tagReceived)
        {
            case "Enemy":
                playerController.ElenaTakingDamage();
                break;
        }
    }

    public override void OnStateExit()
    {
        if (_isUsingSkill)
        {
            _elenaAgentPlacement.SetActive(true);
            myCurrentAgent.enabled = false;
        }

        if (IsElenaAboutToBackstab())
            ResetAssassinating();

        playerController.ElenaSkillButtonController();
        playerController.ElenaIconSelectedOFF();
        playerController.ElenaButtonInteractivitySetter();
        ResetInteractableWhenExitCharacter();
        SavePopupNeeded("Stealth mode");
        SavePopupNeeded("Backstab");
        ResetCharactersControl();
    }

    private bool IsElenaAboutToBackstab()
    {
        return (IsAbleToAssassinTarget || IsAssassinatingTarget);
    }

    private void HighlightCursorOverInteractableObject()
    {
        IsAbleToAssassinTarget = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, playerController.InteractableLayerMask))
        {
            if (hitInfo.collider.GetComponent<IElenaInteractables>() != null)
            {
                if (!isInteracting)
                {
                    if (hitInfo.collider.GetComponent<IElenaAssassin>() != null)
                    {
                        if (IsHavingClearSight(hitInfo.transform))
                        {
                            if (hitInfo.transform.GetComponentInChildren<EnemyTargetDetecting>().IsElenaBeenSpotted) return;
                            IsAbleToAssassinTarget = true;
                            CursorController.Instance.SetAssassinCursor();
                        }
                        else
                            return;
                    }
                    else
                        CursorController.Instance.SetInteractableCursor();

                    if (interactableObject == null)
                        interactableObject = hitInfo.transform;

                    interactableObject.GetComponent<Outline>().enabled = true;
                    isPossibleToInteract = true;
                }
            }
        }
        else
        {
            ResetInteractable();
            CursorController.Instance.SetStandardCursor();
            isPossibleToInteract = false;
        }
    }

    private void ElenaInitialization()
    {
        CharacterComponentsInitialization(CharactersEnum.Elena.ToString());

        _elenaStealthManager = myCurrentCharacter.GetComponent<ElenaStealthManager>();

        _elenaAgentPlacement = myCurrentCharacter.transform.GetChild(2).gameObject;

        playerController.ElenaSkillButtonController();
        playerController.ElenaIconSelectedON();
        playerController.ElenaButtonInteractivitySetter();

        if (_elenaStealthManager.IsInStealthMode)
        {
            _isUsingSkill = true;
            myCurrentAgent.enabled = true;
            _elenaAgentPlacement.SetActive(false);
            playerController.ElenaOnSkillMode();
        }

        initializationComplete = true;
    }

    private void SwitchCharacters()
    {
        if (IsAssassinatingTarget) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && GameObject.FindGameObjectWithTag(CharactersEnum.Douglas.ToString()))
            playerController.SetState(new DouglasState(playerController, cameraController));

        else if (Input.GetKeyDown(KeyCode.Alpha3) && GameObject.FindGameObjectWithTag(CharactersEnum.Hector.ToString()))
            playerController.SetState(new HectorState(playerController, cameraController));
    }

    private void ResetAssassinating()
    {
        IsAbleToAssassinTarget = false;
        IsAssassinatingTarget = false;
        ResetInteractable();
        ResetAIPath();
        CursorController.Instance.SetStandardCursor();
        isPossibleToInteract = false;
    }

    private bool IsHavingClearSight(Transform target)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(myCurrentCharacter.transform.position + (Vector3.up * 1.2f), DirectionToEnemy(target), out hitInfo, 10f, ~playerController.AvoidLayersMasks))
        {
            if (hitInfo.transform == target.transform)
                return true;
        }
        return false;
    }

    private Vector3 DirectionToEnemy(Transform target) => (target.position - myCurrentCharacter.transform.position).normalized;
}
