using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController
{
    #region Elena attributes
    [Space(height: 20)]

    [Header("Elena attributes:", order = 1)]
    [SerializeField] private Image _elenaHP;
    [SerializeField] private Button _elenaButtonRef;

    private int _elenaCurrentHP = MAX_HP;

    [Header("Elena icon properties:", order = 2)]
    [SerializeField] private Image _elenaIconPlaceHolder;

    [SerializeField] private Sprite _elenaStandardIconSprite, _elenaSelectedIconSprite;

    [Header("Elena skill properties:", order = 3)]
    [SerializeField] private Image _elenaSkillPlaceHolder;

    [SerializeField] private Sprite _elenaStandardSkillSprite, _elenaSelectedSkillSprite;

    #endregion

    #region Elena UI manager

    public void ElenaButtonInteractivitySetter()
    {
        if(_elenaButtonRef != null)
            _elenaButtonRef.interactable = _elenaButtonRef.IsInteractable() ? false : true;
    }

    public void SwitchToElenaStateViaButton()
    {
        if (IsLifting) return;

        if (GameObject.FindWithTag("Elena"))
        {
            SetState(new ElenaState(this, _mainCamera));

            if (GameManager.Instance.IsReachedFinalCheckPoint) return;
            if (TutorialPopUpsController.Instance.MyTutorialHandler["Select Elena"])
            {
                TutorialPopUpsController.Instance.DestroyFirstChild();
                TutorialPopUpsController.Instance.DisplayFirstChild();
                SwitchToCharacterTutorial switchToCharacterTutorial = FindObjectOfType<SwitchToCharacterTutorial>();
                switchToCharacterTutorial.ContinueOnTutorial();
            }
        }
    }

    public void ElenaIconSelectedON()
    {
        if (_elenaIconPlaceHolder != null)
            _elenaIconPlaceHolder.sprite = _elenaSelectedIconSprite;
    }

    public void ElenaIconSelectedOFF()
    {
        if (_elenaIconPlaceHolder != null)
            _elenaIconPlaceHolder.sprite = _elenaStandardIconSprite;
    }

    public void ElenaSkillButtonController()
    {
        if (_elenaSkillPlaceHolder != null)
            _elenaSkillPlaceHolder.enabled = !_elenaSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void ElenaOffSkillMode()
    {
        if (_elenaSkillPlaceHolder != null)
            _elenaSkillPlaceHolder.sprite = _elenaStandardSkillSprite;
    }

    public void ElenaOnSkillMode()
    {
        if (_elenaSkillPlaceHolder != null)
            _elenaSkillPlaceHolder.sprite = _elenaSelectedSkillSprite;
    }
    
    public void ElenaTakingDamage(int damageAmount = 1)
    {
        if(_elenaCurrentHP != 0)
        {
            for (int i = 0; i < damageAmount; i++)
            {
                _elenaCurrentHP--;

                if (_elenaHP != null)
                    _elenaHP.sprite = _hpBars[_elenaCurrentHP];

                if (_elenaCurrentHP <= 0)
                {
                    ElenaSFX.PlayRandomDeathClip();
                    SceneController.LoadScene(_buildIndex: 1);
                    break;
                }
            }
        }
    }

    public void ElenaGainingHealth(int regenAmount)
    {
        for (int i = 0; i < regenAmount; i++)
        {
            if (_elenaCurrentHP < 10)
            {
                _elenaCurrentHP++;
                _elenaHP.sprite = _hpBars[_elenaCurrentHP];
            }
        }
    }
    #endregion
}
