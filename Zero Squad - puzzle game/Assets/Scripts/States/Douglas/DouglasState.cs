using UnityEngine.AI;
using UnityEngine;

public class DouglasState : PlayerStateManager
{
    string douglasName = "Douglas";

    private bool _isUsingSkill;
    
    private GameObject _douglasShotgun;
    private GameObject _douglasAgentPlacement;

    private DouglasShootingManager _douglasShootingManager;

    public DouglasState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void UpdateHandle()
    {
        if (!_isUsingSkill)
            PointAndClickMovement();
        else
        {
            TurnTowardTheCursor();
            DouglasPointAndClickShooting();
        }

        EnterOrExitSkillMode();
        SwitchCharacters();
    }

    public override void OnStateEnter()
    {
        DouglasInitialization();
        Debug.Log("Douglas is now in control");
    }

    public override void OnStateExit()
    {
        if (_isUsingSkill)
            _douglasAgentPlacement.SetActive(true);

        playerController.DouglasSkillButtonController();
        playerController.DouglasIconSelectedOFF();
        playerController.DouglasButtonInteractivitySetter();

        myCurrentCharacter = null;
        myCurrentAgent = null;
        animator = null;
        initializationComplete = false;

        Debug.Log("Douglas is out of control");
    }

    private void DouglasInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(douglasName);

        animator = myCurrentCharacter.GetComponent<Animator>();

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();

        _douglasShootingManager = myCurrentCharacter.GetComponent<DouglasShootingManager>();
        _douglasShotgun = myCurrentCharacter.transform.GetChild(2).gameObject;

        _douglasAgentPlacement = myCurrentCharacter.transform.GetChild(3).gameObject;

        playerController.DouglasSkillButtonController();
        playerController.DouglasIconSelectedON();
        playerController.DouglasButtonInteractivitySetter();

        if (_douglasShotgun.activeSelf)
        {
            _isUsingSkill = true;
            _douglasAgentPlacement.SetActive(false);
            playerController.DouglasOnSkillMode();
        }

        initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && initializationComplete || enterSkillViaButton)
        {
            enterSkillViaButton = false;
            _isUsingSkill = !_isUsingSkill ? true : false;

            if (!_douglasShotgun.activeSelf)
            {
                playerController.DouglasOnSkillMode();
                _douglasShotgun.SetActive(true);
            }
            else
            {
                playerController.DouglasOffSkillMode();
                _douglasShotgun.SetActive(false);
                myCurrentAgent.enabled = true;
            }
        }
    }

    private void SwitchCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.SetState(new ElenaState(playerController, cameraController));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.SetState(new HectorState(playerController, cameraController));
        }
    }

    public void DouglasPointAndClickShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _douglasShootingManager.CallSimpleShoot();
        }
    }
}
