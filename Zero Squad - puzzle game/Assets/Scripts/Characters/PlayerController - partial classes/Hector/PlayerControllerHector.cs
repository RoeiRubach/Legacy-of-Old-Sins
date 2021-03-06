﻿using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController
{ 
    #region Hector attributes
    [Space(height: 20)]

    [Header("Hector attributes:", order = 1)]
    [SerializeField] private Image _hectorHP;
    [SerializeField] private Button _hectorButtonRef;

    private int _hectorCurrentHP = MAX_HP;

    [Header("Hector icon properties:", order = 2)]
    [SerializeField] private Image _hectorIconPlaceHolder;

    [SerializeField] private Sprite _hectorStandardIconSprite, _hectorSelectedIconSprite;

    [Header("Hector skill properties:", order = 3)]
    [SerializeField] private Image _hectorSkillPlaceHolder;

    [SerializeField] private Sprite _hectorStandardSkillSprite, _hectorSelectedSkillSprite;

    #endregion

    #region Hector UI manager

    public void HectorButtonInteractivitySetter()
    {
        if(_hectorButtonRef != null)
            _hectorButtonRef.interactable = _hectorButtonRef.IsInteractable() ? false : true;
    }

    public void SwitchToHectorStateViaButton()
    {
        if (IsLifting) return;

        if (GameObject.FindWithTag("Hector"))
        {
            SetState(new HectorState(this, _mainCamera));
            if (GameManager.Instance.IsReachedFinalCheckPoint) return;
            if (TutorialPopUpsController.Instance.MyTutorialHandler["Selections"])
            {
                TutorialPopUpsController.Instance.DestroyFirstChild();
                TutorialPopUpsController.Instance.DisplayFirstChild();
                SwitchToCharacterTutorial switchToCharacterTutorial = FindObjectOfType<SwitchToCharacterTutorial>();
                switchToCharacterTutorial.ContinueOnTutorial();
            }
        }
    }

    public void HectorSkillButtonController()
    {
        if (_hectorSkillPlaceHolder != null)
            _hectorSkillPlaceHolder.enabled = !_hectorSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void HectorIconSelectedON()
    {
        if (_hectorIconPlaceHolder != null)
            _hectorIconPlaceHolder.sprite = _hectorSelectedIconSprite;
    }

    public void HectorIconSelectedOFF()
    {
        if (_hectorIconPlaceHolder != null)
            _hectorIconPlaceHolder.sprite = _hectorStandardIconSprite;
    }

    public void HectorOffSkillMode()
    {
        if (_hectorSkillPlaceHolder != null)
            _hectorSkillPlaceHolder.sprite = _hectorStandardSkillSprite;
    }

    public void HectorOnSkillMode()
    {
        if (_hectorSkillPlaceHolder != null)
            _hectorSkillPlaceHolder.sprite = _hectorSelectedSkillSprite;
    }
    
    public void HectorTakingDamage(int damageAmount = 1)
    {
        if(_hectorCurrentHP != 0)
        {
            for (int i = 0; i < damageAmount; i++)
            {
                _hectorCurrentHP--;

                if (_hectorHP != null)
                    _hectorHP.sprite = _hpBars[_hectorCurrentHP];

                if (_hectorCurrentHP <= 0)
                {
                    HectorSFX.PlayRandomDeathClip();
                    SceneController.LoadScene(_buildIndex: 1);
                    break;
                }
            }
        }
    }
    
    public void HectorGainingHealth(int regenAmount)
    {
        for (int i = 0; i < regenAmount; i++)
        {
            if (_hectorCurrentHP < 10)
            {
                _hectorCurrentHP++;
                _hectorHP.sprite = _hpBars[_hectorCurrentHP];
            }
            else
                break;
        }
    }
    #endregion
}
