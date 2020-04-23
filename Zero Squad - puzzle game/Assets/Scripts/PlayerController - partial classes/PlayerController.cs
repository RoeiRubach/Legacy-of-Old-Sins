﻿using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool IsLifting;
    private const int _maxHP = 10;

    private PlayerStateManager _currentState;

    private CameraController _mainCamera;
    
    [Header("LayerMask:", order = 0)]
    public LayerMask walkableLayerMask;

    [Header("HP bars pool:", order = 1)]
    [SerializeField] private Sprite[] _hpBars;

    private void Start()
    {
        _mainCamera = Camera.main.GetComponent<CameraController>();
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
        _currentState.EnterSkillViaButton = !_currentState.EnterSkillViaButton ? true : false;
    }
}
