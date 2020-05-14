﻿using UnityEngine.AI;
using UnityEngine;

public class DouglasState : PlayerStateManager
{
    private string _douglasName = "Douglas";

    private bool _isUsingSkill;
    
    private GameObject _douglasShotgun;
    private GameObject _douglasAgentPlacement;
    private Transform _bombRef;

    private DouglasShootingManager _douglasShootingManager;
    private DouglasAutoShooting _douglasAutoShooting;

    public DouglasState(PlayerController character) : base(character)
    {
    }
    public DouglasState(PlayerController character, CameraController camera) : base(character, camera)
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
        {
            myCurrentAnimator.SetBool("_isShooting", false);
            TurnTowardTheCursor();
            DouglasPointAndClickShooting();
        }

        if (!isCabinetInteracting)
        {
            EnterOrExitSkillMode();
            SwitchCharacters();
        }
    }

    private void HighlightCursorOverInteractableObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.GetComponent<IDouglasInteractables>() != null)
            {
                CursorController.Instance.SetInteractableCursor();
                interactableObject = hitInfo.transform;
                interactableObject.GetComponent<Outline>().enabled = true;

                if (hitInfo.transform.name == "Bomb")
                    _bombRef = hitInfo.transform;

                isPossibleToInteract = true;
            }
            else
            {
                ResetInteractable();
                CursorController.Instance.SetStandardCursor();
                isPossibleToInteract = false;
            }
        }
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
                playerController.DouglasTakingDamage();
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
        {
            _douglasAgentPlacement.SetActive(true);
            _douglasAutoShooting.enabled = true;
        }

        DouglasUIToggleOFF();

        myCurrentCharacter = null;
        myCurrentAgent = null;
        myCurrentAnimator = null;
        _initializationComplete = false;

        Debug.Log("Douglas is out of control");
    }

    private void DouglasInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(_douglasName);

        myCurrentAnimator = myCurrentCharacter.GetComponent<Animator>();

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();

        DouglasShootingScriptsInitialization();

        _douglasAgentPlacement = myCurrentCharacter.transform.GetChild(2).gameObject;

        DouglasUIToggleON();

        if (_douglasShotgun.activeSelf)
        {
            _isUsingSkill = true;
            _douglasAgentPlacement.SetActive(false);
            _douglasAutoShooting.enabled = false;
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
            isInteracting = false;
            ResetInteractable();

            if (!playerController.IsLifting)
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
            else
            {
                ResetAIPath();
                playerController.IsLifting = false;
                _bombRef.GetComponent<TriggerBomb>().TriggerBombInSeconds();
                _bombRef.parent = null;
                _bombRef.GetComponent<Rigidbody>().useGravity = true;
                myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isLifting.ToString(), false);
                myCurrentAnimator.SetBool(CharactersAnimationTransitionParameters._isCarrying.ToString(), false);
                myCurrentAgent.speed = runningSpeed;
            }
        }
    }

    private void SwitchCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && GameObject.FindGameObjectWithTag("Elena"))
            playerController.SetState(new ElenaState(playerController, cameraController));

        else if (Input.GetKeyDown(KeyCode.Alpha3) && GameObject.FindGameObjectWithTag("Hector"))
            playerController.SetState(new HectorState(playerController, cameraController));
    }

    private void DouglasShootingScriptsInitialization()
    {
        if(_douglasShootingManager == null)
            _douglasShootingManager = myCurrentCharacter.GetComponent<DouglasShootingManager>();

        if(_douglasAutoShooting == null)
            _douglasAutoShooting = myCurrentCharacter.GetComponent<DouglasAutoShooting>();

        if(_douglasShotgun == null)
            _douglasShotgun = _douglasShootingManager.DouglasShotgunRef;
    }

    private void DouglasPointAndClickShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_douglasShootingManager.CallSimpleShoot())
                myCurrentAnimator.SetBool("_isShooting", true);
        }
    }

    private void DouglasUIToggleOFF()
    {
        playerController.DouglasSkillButtonToggle();
        playerController.DouglasIconSelectedOFF();
        playerController.DouglasButtonInteractivityToggle();
    }

    private void DouglasUIToggleON()
    {
        playerController.DouglasSkillButtonToggle();
        playerController.DouglasIconSelectedON();
        playerController.DouglasButtonInteractivityToggle();
    }
}