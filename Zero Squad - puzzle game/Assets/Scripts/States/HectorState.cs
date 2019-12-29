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
        myCurrentCharacter = GameObject.FindWithTag(hectorName);
        cameraController.SetCharacter(myCurrentCharacter);
        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();
        Debug.Log("Hector is now in control");
    }

    public override void OnStateExit()
    {
        myCurrentCharacter = null;
        myCurrentAgent = null;

        Debug.Log("Hector is out of control");
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
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.SetState(new ElenaState(playerController, cameraController));
        }
    }
}
