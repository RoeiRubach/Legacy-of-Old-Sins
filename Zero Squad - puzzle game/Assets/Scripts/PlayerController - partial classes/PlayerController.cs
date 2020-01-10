using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    private const int maxHP = 10;

    private PlayerStateManager currentState;

    [SerializeField]
    private CameraController mainCamera;
    
    [Header("LayerMask", order = 0)]
    public LayerMask walkableLayerMask;

    [Header("HP bars pool", order = 1)]
    [SerializeField]
    private Sprite[] hpBars;

    private void Start()
    {
        SetState(new DouglasState(this, mainCamera));
    }

    private void Update()
    {
        currentState.UpdateHandle();
    }

    public void SetState(PlayerStateManager playerState)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = playerState;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public void EnterSkillViaButton()
    {
        currentState.enterSkillViaButton = !currentState.enterSkillViaButton ? true : false;
    }
}
