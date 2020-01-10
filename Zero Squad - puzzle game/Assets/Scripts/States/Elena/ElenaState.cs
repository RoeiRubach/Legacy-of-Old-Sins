using UnityEngine.AI;
using UnityEngine;

public class ElenaState : PlayerStateManager
{
    string elenaName = "Elena";

    private bool isUsingSkill;

    private GameObject elenaAgentPlacement;

    private ElenaStealthManager elenaStealthManager;

    public ElenaState(PlayerController character, CameraController camera) : base(character, camera)
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
        ElenaInitialization();
        Debug.Log("Elena is now in control");
    }

    public override void OnStateExit()
    {
        if (isUsingSkill)
            elenaAgentPlacement.SetActive(true);

        playerController.ElenaSkillButtonController();
        playerController.ElenaIconSelectedOFF();
        playerController.ElenaButtonInteractivitySetter();

        myCurrentCharacter = null;
        myCurrentAgent = null;
        initializationComplete = false;

        Debug.Log("Elena is out of control");
    }

    private void ElenaInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(elenaName);

        cameraController.SetCharacter(myCurrentCharacter);

        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();

        elenaStealthManager = myCurrentCharacter.GetComponent<ElenaStealthManager>();

        elenaAgentPlacement = myCurrentCharacter.transform.GetChild(2).gameObject;

        playerController.ElenaSkillButtonController();
        playerController.ElenaIconSelectedON();
        playerController.ElenaButtonInteractivitySetter();

        if (elenaStealthManager.IsElenaUsingStealth())
        {
            isUsingSkill = true;
            elenaAgentPlacement.SetActive(false);
            playerController.ElenaOnSkillMode();
        }

        initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && initializationComplete || enterSkillViaButton)
        {
            enterSkillViaButton = false;
            isUsingSkill = !isUsingSkill ? true : false;

            if (isUsingSkill)
            {
                myCurrentAgent.enabled = false;
                elenaStealthManager.CallStealthMode();
                playerController.ElenaOnSkillMode();
            }
            else
            {
                elenaStealthManager.OffStealthMode();
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
