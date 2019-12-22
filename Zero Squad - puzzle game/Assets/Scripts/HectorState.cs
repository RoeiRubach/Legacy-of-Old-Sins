using UnityEngine.AI;
using UnityEngine;

public class HectorState : PlayerStateManager
{
    string hectorName = "Hector";

    public HectorState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void Tick()
    {
        PointAndClickMovement();
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

    private void SwitchCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.SetState(new ElenaState(playerController, cameraController));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.SetState(new DouglasState(playerController, cameraController));
        }
    }
}
