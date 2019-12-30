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
        ElenaInitialization();
        Debug.Log("Elena is now in control");
    }

    public override void OnStateExit()
    {
        if (isUsingSkill)
            elenaAgentPlacement.SetActive(true);
        else
            myCurrentAgent.enabled = true;

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

        if (elenaStealthManager.IsElenaUsingStealth())
            isUsingSkill = true;

        initializationComplete = true;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space) && initializationComplete)
        {
            isUsingSkill = !isUsingSkill ? true : false;

            if (isUsingSkill)
            {
                myCurrentAgent.enabled = false;
                elenaStealthManager.CallStealthMode();
            }
            else
            {
                elenaStealthManager.OffStealthMode();
                elenaAgentPlacement.SetActive(false);
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
