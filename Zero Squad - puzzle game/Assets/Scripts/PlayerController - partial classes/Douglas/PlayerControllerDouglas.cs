using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController
{
    #region Douglas attributes

    [Header("Douglas attributes:", order = 0)]
    [SerializeField] private Image _douglasHP;
    [SerializeField] private Button _douglasButtonRef;

    private int _douglasCurrentHP = _maxHP;

    [Header("Douglas icon properties:", order = 1)]
    [SerializeField] private Image _douglasIconPlaceHolder;

    [SerializeField] private Sprite _douglasStandardIconSprite;
    [SerializeField] private Sprite _douglasSelectedIconSprite;

    [Header("Douglas skill properties:", order = 2)]
    [SerializeField] private Image _douglasSkillPlaceHolder;

    [SerializeField] private Sprite _douglasStandardSkillSprite;
    [SerializeField] private Sprite _douglasSelectedSkillSprite;

    #endregion

    #region Douglas UI manager

    public void DouglasButtonInteractivitySetter()
    {
        _douglasButtonRef.interactable = _douglasButtonRef.IsInteractable() ? false : true;
    }

    public void SwitchToDouglasStateViaButton()
    {
        SetState(new DouglasState(this, _mainCamera));
    }

    public void DouglasIconSelectedON()
    {
        _douglasIconPlaceHolder.GetComponent<Image>().sprite = _douglasSelectedIconSprite;
    }

    public void DouglasIconSelectedOFF()
    {
        _douglasIconPlaceHolder.GetComponent<Image>().sprite = _douglasStandardIconSprite;
    }

    public void DouglasSkillButtonController()
    {
        _douglasSkillPlaceHolder.enabled = !_douglasSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void DouglasSpriteOffSkillMode()
    {
        _douglasSkillPlaceHolder.GetComponent<Image>().sprite = _douglasStandardSkillSprite;
    }

    public void DouglasSpriteOnSkillMode()
    {
        _douglasSkillPlaceHolder.GetComponent<Image>().sprite = _douglasSelectedSkillSprite;
    }

    [ContextMenu("Apply damage to Douglas - PLAYMODE ONLY!")]
    public void DouglasTakingDamageTest()
    {
        if (_douglasCurrentHP > 0)
        {
            _douglasCurrentHP--;
            _douglasHP.GetComponent<Image>().sprite = _hpBars[_douglasCurrentHP];
        }
        else
            print("Douglas is already dead you sick fuck");
    }

    #endregion
}
