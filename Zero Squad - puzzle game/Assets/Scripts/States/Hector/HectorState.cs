﻿using UnityEngine.AI;
using UnityEngine;

public class HectorState : PlayerStateManager
{
    string hectorName = "Hector";

    private bool isUsingSkill;

    private GameObject hectorShield;
    private GameObject hectorAgentPlacement;

    public HectorState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void Handle()
    {
        if (!isUsingSkill)
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

    public override void OnStateExit()
    {
        if (isUsingSkill)
            hectorAgentPlacement.SetActive(true);
        else
            myCurrentAgent.enabled = true;

        myCurrentCharacter = null;
        myCurrentAgent = null;
        initializationComplete = false;

        Debug.Log("Hector is out of control");
    }

    private void HectorInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(hectorName);

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();

        hectorShield = myCurrentCharacter.transform.GetChild(2).gameObject;

        hectorAgentPlacement = myCurrentCharacter.transform.GetChild(3).gameObject;

        if (hectorShield.activeSelf)
        {
            isUsingSkill = true;
            hectorAgentPlacement.SetActive(false);
        }

        initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && initializationComplete)
        {
            isUsingSkill = !isUsingSkill ? true : false;

            if (!hectorShield.activeSelf)
            {
                myCurrentAgent.enabled = false;
                hectorShield.SetActive(true);
            }
            else
            {
                hectorShield.SetActive(false);
                myCurrentAgent.enabled = true;
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
