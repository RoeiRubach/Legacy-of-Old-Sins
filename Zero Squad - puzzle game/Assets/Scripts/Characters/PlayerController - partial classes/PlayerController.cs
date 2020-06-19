using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool IsLifting;
    [SerializeField] private Transform[] _charactersRef;
    [SerializeField] private GameObject _hectorUIRef, _elenaUIRef;

    private const int _maxHP = 10;

    private PlayerStateManager _currentState;

    public PlayerStateManager GetCurrentStateRef() => _currentState;

    private CameraController _mainCamera;
    
    [Header("LayerMasks:", order = 0)]
    public LayerMask WalkableLayerMask;
    public LayerMask InteractableLayerMask;
    public LayerMask AvoidLayersMasks;

    [Header("HP bars pool:", order = 1)]
    [SerializeField] private Sprite[] _hpBars;
    
    private void Start()
    {
        _mainCamera = Camera.main.GetComponent<CameraController>();
        SetState(new DouglasState(this, _mainCamera));

        if(GameManager.Instance != null)
            if (GameManager.Instance.IsReachedCheckPoint)
            {
                _douglasCurrentHP = _maxHP;
                if (_douglasHP != null)
                    _douglasHP.sprite = _hpBars[_douglasCurrentHP];

                _hectorUIRef.SetActive(true);
                _elenaUIRef.SetActive(true);
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

    public void EnterSkillViaButton(string tutorialPopUp = null)
    {
        _currentState.EnterSkillViaButton = !_currentState.EnterSkillViaButton ? true : false;

        if (tutorialPopUp == null) return;
        if (GameManager.Instance.IsReachedFinalCheckPoint) return;
        if (TutorialPopUpsController.Instance.MyTutorialHandler[tutorialPopUp])
        {
            TutorialPopUpsController.Instance.DestroyFirstChild();
            TutorialPopUpsController.Instance.DisplayFirstChild();
        }
    }

    public void ElenaStealthTimerIsOver()
    {
        if (!_currentState.IsCabinetInteracting)
        {
            CancelInvoke();
            InvokeElenaSwitch();
            EnterSkillViaButton();
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
            EnterSkillViaButton();
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

    public void SwitchToHectorTutorial() => Invoke("InvokeHectorSwitch", 0.5f);

    public void SwitchToElenaTutorial() => Invoke("InvokeElenaSwitch", 0.5f);

    private void InvokeHectorSwitch() => SetState(new HectorState(this, _mainCamera));

    private void InvokeElenaSwitch() => SetState(new ElenaState(this, _mainCamera));

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
