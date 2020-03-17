using UnityEngine.AI;
using UnityEngine;

public class DouglasState : PlayerStateManager
{
    private string douglasName = "Douglas";

    private bool _isUsingSkill;
    
    private GameObject _douglasShotgun;
    private GameObject _douglasAgentPlacement;

    private DouglasShootingManager _douglasShootingManager;

    public DouglasState(PlayerController character) : base(character)
    {

    }
    public DouglasState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void UpdateHandle()
    {
        if (!_isUsingSkill)
            PointAndClickMovement();
        else
        {
            myCurrentAnimator.SetBool("_isShooting", false);
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

    public override void OnTriggerEnter(string tagReceived, HealthRegenCollectables healthRegenCollectables)
    {
        switch (tagReceived)
        {
            case "Enemy":
                playerController.DouglasTakingDamageTest();
                break;
            case "HealthRegen":
                Debug.Log(healthRegenCollectables.HealthToRegen);
                playerController.DouglasGainingHealth(healthRegenCollectables.HealthToRegen);
                healthRegenCollectables.CallOnDestroy();
                break;
        }
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
        myCurrentAnimator = null;
        _initializationComplete = false;

        Debug.Log("Douglas is out of control");
    }

    private void DouglasInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(douglasName);

        myCurrentAnimator = myCurrentCharacter.GetComponent<Animator>();

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();

        _douglasShootingManager = myCurrentCharacter.GetComponent<DouglasShootingManager>();
        _douglasShotgun = _douglasShootingManager.DouglasShotgunRef;

        _douglasAgentPlacement = myCurrentCharacter.transform.GetChild(2).gameObject;

        playerController.DouglasSkillButtonController();
        playerController.DouglasIconSelectedON();
        playerController.DouglasButtonInteractivitySetter();

        if (_douglasShotgun.activeSelf)
        {
            _isUsingSkill = true;
            _douglasAgentPlacement.SetActive(false);
            playerController.DouglasSpriteOnSkillMode();
        }
        else
            _douglasShootingManager.enabled = false;

        _initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _initializationComplete || EnterSkillViaButton)
        {
            EnterSkillViaButton = false;
            _isUsingSkill = !_isUsingSkill ? true : false;

            if (!_douglasShotgun.activeSelf)
            {
                playerController.DouglasSpriteOnSkillMode();
                _douglasShootingManager.enabled = true;
                _douglasShotgun.SetActive(true);
            }
            else
            {
                playerController.DouglasSpriteOffSkillMode();
                _douglasShootingManager.enabled = false;
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

    private void DouglasPointAndClickShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_douglasShootingManager.CallSimpleShoot())
                myCurrentAnimator.SetBool("_isShooting", true);
        }
    }
}
