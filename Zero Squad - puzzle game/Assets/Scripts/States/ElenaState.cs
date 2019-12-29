using UnityEngine.AI;
using UnityEngine;

public class ElenaState : PlayerStateManager
{
    string elenaName = "Elena";

    private bool isUsingSkill;

    public ElenaState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void Handle()
    {
        //(!isUsingSkill) ? PointAndClickMovement() : TurnTowardTheCursor();

        if (!isUsingSkill)
            PointAndClickMovement();
        else
            TurnTowardTheCursor();

        SpaceKeyToEnterOrExitSkill();
        SwitchCharacters();
    }

    public override void OnStateEnter()
    {
        myCurrentCharacter = GameObject.FindWithTag(elenaName);
        cameraController.SetCharacter(myCurrentCharacter);
        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();
        Debug.Log("Elena is now in control");
    }

    public override void OnStateExit()
    {
        myCurrentCharacter = null;
        myCurrentAgent = null;

        Debug.Log("Elena is out of control");
    }

    public override void SpaceKeyToEnterOrExitSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isUsingSkill = !isUsingSkill ? true : false;
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
