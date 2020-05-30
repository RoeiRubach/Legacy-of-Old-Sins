using UnityEngine;

public class ElenaState : PlayerStateManager
{
    private string _elenaName = "Elena";
    private bool _isUsingSkill;
    private GameObject _elenaAgentPlacement;
    private ElenaStealthManager _elenaStealthManager;

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
            if (interactableObject.name == "Mindless possessed")
            {
                if (interactableObject.GetComponentInChildren<EnemyTargetDetecting>().IsElenaBeenSpotted)
                {
                    isInteracting = false;
                    ResetInteractable();
                    ResetAIPath();
                }
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

                if (_isUsingSkill)
                {
                    ResetAIPath();
                    myCurrentAgent.speed = stealthRunningSpeed;
                    _elenaStealthManager.CallStealthMode();
                    playerController.ElenaOnSkillMode();
                }
                else
                {
                    myCurrentAgent.speed = runningSpeed;
                    _elenaStealthManager.OffStealthMode();
                    myCurrentAgent.enabled = true;
                    playerController.ElenaOffSkillMode();
                }
            }
        }
    }

    public override void OnStateEnter()
    {
        ElenaInitialization();
        //Debug.Log("Elena is now in control");
    }

    public override void OnTriggerEnter(string tagReceived, HealthRegenCollectables healthRegenCollectables)
    {
        switch (tagReceived)
        {
            case "Enemy":
                playerController.ElenaTakingDamage();
                break;
            case "HealthRegen":
                Debug.Log(healthRegenCollectables.HealthToRegen);
                playerController.ElenaGainingHealth(healthRegenCollectables.HealthToRegen);
                healthRegenCollectables.CallOnDestroy();
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

        playerController.ElenaSkillButtonController();
        playerController.ElenaIconSelectedOFF();
        playerController.ElenaButtonInteractivitySetter();

        ResetCharactersControl();

        //Debug.Log("Elena is out of control");
    }

    private void HighlightCursorOverInteractableObject()
    {
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
                        if (hitInfo.transform.GetComponentInChildren<EnemyTargetDetecting>().IsElenaBeenSpotted)
                            return;
                        CursorController.Instance.SetAssassinCursor();
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
        CharacterComponentsInitialization(_elenaName);

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
        if (!isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && GameObject.FindGameObjectWithTag("Douglas"))
                playerController.SetState(new DouglasState(playerController, cameraController));

            else if (Input.GetKeyDown(KeyCode.Alpha3) && GameObject.FindGameObjectWithTag("Hector"))
                playerController.SetState(new HectorState(playerController, cameraController));
        }
    }

    private void ResetAssassinating()
    {
        ResetInteractable();
        CursorController.Instance.SetStandardCursor();
        isPossibleToInteract = false;
    }
}
