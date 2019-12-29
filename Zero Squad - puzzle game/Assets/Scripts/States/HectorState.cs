using UnityEngine.AI;
using UnityEngine;

public class HectorState : PlayerStateManager
{
    string hectorName = "Hector";

    private bool isUsingSkill;

    public HectorState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void Handle()
    {
        if (!isUsingSkill)
            PointAndClickMovement();
        else
        {
            TurnTowardTheCursor();
        }

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
        myCurrentCharacter = null;
        myCurrentAgent = null;

        Debug.Log("Hector is out of control");
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isUsingSkill = !isUsingSkill ? true : false;
    }

    private void HectorInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(hectorName);
        cameraController.SetCharacter(myCurrentCharacter);
        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();
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
