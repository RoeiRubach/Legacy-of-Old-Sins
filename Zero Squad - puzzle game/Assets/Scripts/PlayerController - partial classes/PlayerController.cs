using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    private const int _maxHP = 10;

    private PlayerStateManager _currentState;

    [SerializeField] private CameraController _mainCamera;
    
    [Header("LayerMask", order = 0)]
    public LayerMask walkableLayerMask;

    [Header("HP bars pool", order = 1)]
    [SerializeField] private Sprite[] _hpBars;

    private void Start()
    {
        SetState(new DouglasState(this, _mainCamera));
    }

    private void Update()
    {
        _currentState.UpdateHandle();
    }

    public void SetState(PlayerStateManager playerState)
    {
        if (_currentState != null)
            _currentState.OnStateExit();

        _currentState = playerState;

        if (_currentState != null)
            _currentState.OnStateEnter();
    }

    public void EnterSkillViaButton()
    {
        _currentState.enterSkillViaButton = !_currentState.enterSkillViaButton ? true : false;
    }
}
