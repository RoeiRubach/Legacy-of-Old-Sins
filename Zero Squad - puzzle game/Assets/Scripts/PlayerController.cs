using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PlayerStateManager currentState;

    [SerializeField]
    private CameraController mainCamera;
    
    public LayerMask walkableLayerMask;

    private void Start()
    {
        SetState(new ElenaState(this, mainCamera));
    }

    private void Update()
    {
        currentState.Tick();
    }

    public void SetState(PlayerStateManager playerState)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = playerState;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}
