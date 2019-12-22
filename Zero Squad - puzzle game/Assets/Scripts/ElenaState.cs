using UnityEngine.AI;
using UnityEngine;

public class ElenaState : PlayerStateManager
{
    string elenaName = "Elena";

    public ElenaState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void Tick()
    {
        PointAndClickMovement();
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

    private void SwitchCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.SetState(new DouglasState(playerController, cameraController));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.SetState(new HectorState(playerController, cameraController));
        }
    }
}
