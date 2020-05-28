using UnityEngine.AI;
using UnityEngine;

public class HectorState : PlayerStateManager
{
    private string _hectorName = "Hector";

    private bool _isUsingSkill;

    private GameObject _hectorShield;
    private Transform _hectorEnemyHittingSpot;
    private GameObject _hectorAgentPlacement;

    public HectorState(PlayerController character) : base(character)
    {

    }
    public HectorState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void UpdateHandle()
    {
        if (!_isUsingSkill)
        {
            HighlightCursorOverInteractableObject();
            PointAndClickMovement();
        }
        else
            TurnTowardTheCursor();

        EnterOrExitSkillMode();
        SwitchCharacters();
    }

    private void HighlightCursorOverInteractableObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, playerController.InteractableLayerMask))
        {
            if (hitInfo.collider.GetComponent<IHectorInteractables>() != null)
            {
                if(hitInfo.collider.GetComponent<IHectorTech>() != null)
                    CursorController.Instance.SetTechCursor();
                else
                    CursorController.Instance.SetInteractableCursor();
                interactableObject = hitInfo.transform;
                interactableObject.GetComponent<Outline>().enabled = true;
                
                isPossibleToInteract = true;
            }
        }
        else
        {
            ResetInteractable();
            CursorController.Instance.SetStandardCursor();
            isPossibleToInteract = false;
        }
    }

    public override void OnStateEnter()
    {
        HectorInitialization();
        //Debug.Log("Hector is now in control");
    }

    public override void OnTriggerEnter(string tagReceived, HealthRegenCollectables healthRegenCollectables)
    {
        switch (tagReceived)
        {
            case "Enemy":
                playerController.HectorTakingDamage();
                break;
            case "HealthRegen":
                Debug.Log(healthRegenCollectables.HealthToRegen);
                playerController.HectorGainingHealth(healthRegenCollectables.HealthToRegen);
                healthRegenCollectables.CallOnDestroy();
                break;
        }
    }

    public override void OnStateExit()
    {
        if (_isUsingSkill)
            _hectorAgentPlacement.SetActive(true);

        playerController.HectorSkillButtonController();
        playerController.HectorIconSelectedOFF();
        playerController.HectorButtonInteractivitySetter();

        myCurrentCharacter = null;
        myCurrentAgent = null;
        myCurrentAnimator = null;
        initializationComplete = false;

        //Debug.Log("Hector is out of control");
    }

    private void HectorInitialization()
    {
        myCurrentCharacter = GameObject.Find(_hectorName);

        myCurrentAnimator = myCurrentCharacter.GetComponent<Animator>();

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();
        
        _hectorShield = myCurrentCharacter.transform.GetChild(2).transform.GetChild(0).gameObject;
        _hectorEnemyHittingSpot = _hectorShield.transform.GetChild(0);

        _hectorAgentPlacement = myCurrentCharacter.transform.GetChild(3).gameObject;

        playerController.HectorSkillButtonController();
        playerController.HectorIconSelectedON();
        playerController.HectorButtonInteractivitySetter();

        if (_hectorShield.activeSelf)
        {
            _isUsingSkill = true;
            _hectorAgentPlacement.SetActive(false);
            playerController.HectorOnSkillMode();
        }

        initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (initializationComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space) || EnterSkillViaButton)
            {
                if (_hectorEnemyHittingSpot.GetComponent<EnemyHealth>().GetCurrentHealth > 0 || _hectorShield.activeSelf)
                {
                    EnterSkillViaButton = false;
                    _isUsingSkill = !_isUsingSkill ? true : false;

                    if (!_hectorShield.activeSelf)
                    {
                        _hectorShield.SetActive(true);
                        playerController.HectorOnSkillMode();
                    }
                    else
                    {
                        _hectorShield.SetActive(false);
                        myCurrentAgent.enabled = true;
                        playerController.HectorOffSkillMode();
                    }
                }
            }
        }
    }

    private void SwitchCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameObject.FindGameObjectWithTag("Douglas"))
            playerController.SetState(new DouglasState(playerController, cameraController));

        else if (Input.GetKeyDown(KeyCode.Alpha2) && GameObject.FindGameObjectWithTag("Elena"))
            playerController.SetState(new ElenaState(playerController, cameraController));
    }
}
