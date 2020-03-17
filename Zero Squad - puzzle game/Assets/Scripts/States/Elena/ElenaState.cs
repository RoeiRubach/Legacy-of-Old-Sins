using UnityEngine.AI;
using UnityEngine;

public class ElenaState : PlayerStateManager
{
    private string elenaName = "Elena";

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
        if (!_isUsingSkill)
            PointAndClickMovement();
        else
            TurnTowardTheCursor();

        EnterOrExitSkillMode();
        SwitchCharacters();
    }

    public override void OnStateEnter()
    {
        ElenaInitialization();
        Debug.Log("Elena is now in control");
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
            _elenaAgentPlacement.SetActive(true);

        playerController.ElenaSkillButtonController();
        playerController.ElenaIconSelectedOFF();
        playerController.ElenaButtonInteractivitySetter();

        myCurrentCharacter = null;
        myCurrentAgent = null;
        myCurrentAnimator = null;
        _initializationComplete = false;

        Debug.Log("Elena is out of control");
    }

    private void ElenaInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(elenaName);

        myCurrentAnimator = myCurrentCharacter.GetComponent<Animator>();

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();

        _elenaStealthManager = myCurrentCharacter.GetComponent<ElenaStealthManager>();

        _elenaAgentPlacement = myCurrentCharacter.transform.GetChild(2).gameObject;

        playerController.ElenaSkillButtonController();
        playerController.ElenaIconSelectedON();
        playerController.ElenaButtonInteractivitySetter();

        if (_elenaStealthManager.IsInStealthMode)
        {
            _isUsingSkill = true;
            _elenaAgentPlacement.SetActive(false);
            playerController.ElenaOnSkillMode();
        }

        _initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _initializationComplete || EnterSkillViaButton)
        {
            EnterSkillViaButton = false;
            _isUsingSkill = !_isUsingSkill ? true : false;

            if (_isUsingSkill)
            {
                _elenaStealthManager.CallStealthMode();
                playerController.ElenaOnSkillMode();
            }
            else
            {
                _elenaStealthManager.OffStealthMode();
                myCurrentAgent.enabled = true;
                playerController.ElenaOffSkillMode();
            }
        }
    }

    private void SwitchCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.SetState(new DouglasState(playerController, cameraController));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.SetState(new HectorState(playerController, cameraController));
        }
    }
}
