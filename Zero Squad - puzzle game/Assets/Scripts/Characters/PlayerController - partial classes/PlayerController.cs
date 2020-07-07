using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    private const int MAX_HP = 10; 

    [HideInInspector] public bool IsLifting;
    public GameObject BombRef { get; set; }
    [SerializeField] private Transform[] _charactersRef;
    [SerializeField] private GameObject _hectorUIRef, _elenaUIRef;

    private PlayerStateManager _currentState;

    public PlayerStateManager GetCurrentStateRef() => _currentState;

    private CameraController _mainCamera;
    public DouglasSFX DouglasSFX { get; private set; }
    public ElenaSFX ElenaSFX { get; private set; }
    public HectorSFX HectorSFX { get; private set; }

    [Header("LayerMasks:", order = 0)]
    public LayerMask WalkableLayerMask;
    public LayerMask InteractableLayerMask;
    public LayerMask AvoidLayersMasks;

    [Header("HP bars pool:", order = 1)]
    [SerializeField] private Sprite[] _hpBars;
    
    private void Start()
    {
        DouglasSFX = (DouglasSFX)FindObjectOfType(typeof(DouglasSFX));
        _mainCamera = Camera.main.GetComponent<CameraController>();
        SetState(new DouglasState(this, _mainCamera));

        if(GameManager.Instance != null)
            if (GameManager.Instance.IsReachedCheckPoint)
            {
                _douglasCurrentHP = MAX_HP;
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
                MusicAudioController.Instance.SwitchGameMusic(GameManager.Instance.CheckPointNumber);
                SetElenaSFXComponent();
                SetHectorSFXComponent();
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

    public void SetElenaSFXComponent() => ElenaSFX = (ElenaSFX)FindObjectOfType(typeof(ElenaSFX));

    public void SetHectorSFXComponent() => HectorSFX = (HectorSFX)FindObjectOfType(typeof(HectorSFX));

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
