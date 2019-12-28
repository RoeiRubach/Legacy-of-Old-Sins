using UnityEngine.AI;
using UnityEngine;

public class DouglasState : PlayerStateManager
{
    string douglasName = "Douglas";

    public DouglasState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void Handle()
    {
        PointAndClickMovement();
        SwitchCharacters();
    }

    public override void OnStateEnter()
    {
        myCurrentCharacter = GameObject.FindWithTag(douglasName);
        cameraController.SetCharacter(myCurrentCharacter);
        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();
        Debug.Log("Douglas is now in control");
    }

    public override void OnStateExit()
    {
        myCurrentCharacter = null;
        myCurrentAgent = null;

        Debug.Log("Douglas is out of control");
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
}
