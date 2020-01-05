using UnityEngine.AI;
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

    public override void UpdateHandle()
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

        playerController.hectorSkillButtonController();
        playerController.HectorIconSelectedOFF();
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

        playerController.hectorSkillButtonController();
        playerController.HectorIconSelectedON();

        if (hectorShield.activeSelf)
        {
            isUsingSkill = true;
            hectorAgentPlacement.SetActive(false);
            playerController.hectorOnSkillMode();
        }

        initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && initializationComplete || enterSkillViaButton)
        {
            enterSkillViaButton = false;
            isUsingSkill = !isUsingSkill ? true : false;

            if (!hectorShield.activeSelf)
            {
                myCurrentAgent.enabled = false;
                hectorShield.SetActive(true);
                playerController.hectorOnSkillMode();
            }
            else
            {
                hectorShield.SetActive(false);
                myCurrentAgent.enabled = true;
                playerController.hectorOffSkillMode();
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
