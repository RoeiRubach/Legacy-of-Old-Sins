using UnityEngine.AI;
using UnityEngine;

public class DouglasState : PlayerStateManager
{
    string douglasName = "Douglas";

    private bool isUsingSkill;
    
    private GameObject douglasShotgun;

    private DouglasShootingManager douglasShootingManager;

    public DouglasState(PlayerController character, CameraController camera) : base(character, camera)
    {
    }

    public override void Handle()
    {
        if (!isUsingSkill)
            PointAndClickMovement();
        else
        {
            TurnTowardTheCursor();
            DouglasPointAndClickShooting();
        }

        EnterOrExitSkillMode();
        SwitchCharacters();
    }

    public override void OnStateEnter()
    {
        DouglasInitialization();
        Debug.Log("Douglas is now in control");
    }

    public override void OnStateExit()
    {
        myCurrentCharacter = null;
        myCurrentAgent = null;

        Debug.Log("Douglas is out of control");
    }

    private void DouglasInitialization()
    {
        myCurrentCharacter = GameObject.FindWithTag(douglasName);
        cameraController.SetCharacter(myCurrentCharacter);
        myCurrentAgent = myCurrentCharacter.GetComponent<NavMeshAgent>();
        douglasShootingManager = myCurrentCharacter.GetComponent<DouglasShootingManager>();

        douglasShotgun = myCurrentCharacter.transform.GetChild(2).gameObject;
    }

    public override void EnterOrExitSkillMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUsingSkill = !isUsingSkill ? true : false;

            if (!douglasShotgun.activeSelf)
                douglasShotgun.SetActive(true);
            else
                douglasShotgun.SetActive(false);
        }
    }

    public void DouglasPointAndClickShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            douglasShootingManager.CallSimpleShoot();
        }
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
