using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController
{
    #region Elena attributes
    [Space(height: 20)]

    [Header("Elena attributes", order = 1)]
    [SerializeField]
    private Image elenaHP;
    [SerializeField]
    private Button elenaButtonRef;

    private int elenaCurrentHP = maxHP;

    [Header("Elena icon properties", order = 2)]
    [SerializeField]
    private Image elenaIconPlaceHolder;

    [SerializeField]
    private Sprite elenaStandardIconSprite, elenaSelectedIconSprite;

    [Header("Elena skill properties", order = 3)]
    [SerializeField]
    private Image elenaSkillPlaceHolder;

    [SerializeField]
    private Sprite elenaStandardSkillSprite, elenaSelectedSkillSprite;

    #endregion

    #region Elena UI manager

    public void ElenaButtonInteractivitySetter()
    {
        elenaButtonRef.interactable = elenaButtonRef.IsInteractable() ? false : true;
    }

    public void SwitchToElenaStateViaButton()
    {
        SetState(new ElenaState(this, mainCamera));
    }

    public void ElenaIconSelectedON()
    {
        elenaIconPlaceHolder.GetComponent<Image>().sprite = elenaSelectedIconSprite;
    }

    public void ElenaIconSelectedOFF()
    {
        elenaIconPlaceHolder.GetComponent<Image>().sprite = elenaStandardIconSprite;
    }

    public void ElenaSkillButtonController()
    {
        elenaSkillPlaceHolder.enabled = !elenaSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void ElenaOffSkillMode()
    {
        elenaSkillPlaceHolder.GetComponent<Image>().sprite = elenaStandardSkillSprite;
    }

    public void ElenaOnSkillMode()
    {
        elenaSkillPlaceHolder.GetComponent<Image>().sprite = elenaSelectedSkillSprite;
    }

    [ContextMenu("Apply damage to Elena - PLAYMODE ONLY!")]
    public void ElenaTakingDamage()
    {
        if (elenaCurrentHP > 0)
        {
            elenaCurrentHP--;
            elenaHP.GetComponent<Image>().sprite = hpBars[elenaCurrentHP];
        }
        else
            print("Elena is already dead you sick fuck");
    } 

    #endregion
}
