using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool IsLifting;
    [SerializeField] private Transform[] _charactersRef;

    private const int _maxHP = 10;

    private PlayerStateManager _currentState;

    private CameraController _mainCamera;
    
    [Header("LayerMasks:", order = 0)]
    public LayerMask WalkableLayerMask;
    public LayerMask InteractableLayerMask;

    [Header("HP bars pool:", order = 1)]
    [SerializeField] private Sprite[] _hpBars;
    
    private void Start()
    {
        _mainCamera = Camera.main.GetComponent<CameraController>();
        SetState(new DouglasState(this, _mainCamera));

        if (GameManager.Instance.IsReachedCheckPoint)
        {
            _douglasCurrentHP = _maxHP;
            if (_douglasHP != null)
                _douglasHP.sprite = _hpBars[_douglasCurrentHP];

            for (int i = 0; i < _charactersRef.Length; i++)
            {
                _charactersRef[i].gameObject.SetActive(true);
                _charactersRef[i].GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                _charactersRef[i].position = GameManager.Instance.CharactersPlacements[i];
                _charactersRef[i].GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            }
        }
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

    public void ElenaStealthTimerIsOver()
    {
        if (!_currentState.IsCabinetInteracting)
        {
            CancelInvoke();
            InvokeElenaSwitch();
            _currentState.EnterSkillViaButton = !_currentState.EnterSkillViaButton ? true : false;
        }
        else
            InvokeRepeating("InvokeElenaStealthSwitching", 0.5f, 1f);
    }

    public void HectorShieldGotDestroy()
    {
        if (!_currentState.IsCabinetInteracting)
        {
            CancelInvoke();
            InvokeHectorSwitch();
            _currentState.EnterSkillViaButton = !_currentState.EnterSkillViaButton ? true : false;
        }
        else
            InvokeRepeating("InvokeShieldDestroying", 0.5f, 1f);
    }

    private void InvokeElenaStealthSwitching()
    {
        if (!IsInvoking("ElenaStealthTimerIsOver"))
            Invoke("ElenaStealthTimerIsOver", 0.5f);
    }

    private void InvokeShieldDestroying()
    {
        if(!IsInvoking("HectorShieldGotDestroy"))
            Invoke("HectorShieldGotDestroy", 0.5f);
    }

    public void SwitchToHectorTutorial()
    {
        Invoke("InvokeHectorSwitch", 0.5f);
    }

    public void SwitchToElenaTutorial()
    {
        Invoke("InvokeElenaSwitch", 0.5f);
    }

    private void InvokeHectorSwitch()
    {
        SetState(new HectorState(this, _mainCamera));
        EnterSkillViaButton();
    }

    private void InvokeElenaSwitch()
    {
        SetState(new ElenaState(this, _mainCamera));
    }

    public void DamageACharacter(Transform target, int damageAmount = 1)
    {
        switch (target.tag)
        {
            case "Douglas":
                DouglasTakingDamage(damageAmount);
                break;
            case "Elena":
                ElenaTakingDamage(damageAmount);
                break;
            case "Hector":
                HectorTakingDamage(damageAmount);
                break;
            case "Hector Shield":
                target.GetComponent<EnemyHealth>().HealthDecreaseViaBullet(damageAmount);
                break;
        }
    }
}
