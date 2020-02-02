using UnityEngine.AI;
using UnityEngine;

public class HectorState : PlayerStateManager
{
    private string hectorName = "Hector";

    private bool _isUsingSkill;

    private GameObject _hectorShield;
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
            PointAndClickMovement();
        else
            TurnTowardTheCursor();

        EnterOrExitSkillMode();
        SwitchCharacters();
    }

    public override void OnStateEnter()
    {
        HectorInitialization();
        Debug.Log("Hector is now in control");
    }

    public override void OnTriggerEnter()
    {
        playerController.HectorTakingDamage();
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
        _initializationComplete = false;

        Debug.Log("Hector is out of control");
    }

    private void HectorInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(hectorName);

        myCurrentAnimator = myCurrentCharacter.GetComponent<Animator>();

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();

        _hectorShield = myCurrentCharacter.transform.GetChild(2).transform.GetChild(0).gameObject;

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

        _initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _initializationComplete || EnterSkillViaButton)
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

    private void SwitchCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.SetState(new DouglasState(playerController, cameraController));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.SetState(new ElenaState(playerController, cameraController));
        }
    }
}
