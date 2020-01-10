using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController
{ 
    #region Hector attributes
    [Space(height: 20)]

    [Header("Hector attributes", order = 1)]
    [SerializeField]
    private Image hectorHP;
    [SerializeField]
    private Button hectorButtonRef;

    private int hectorCurrentHP = maxHP;

    [Header("Hector icon properties", order = 2)]
    [SerializeField]
    private Image hectorIconPlaceHolder;

    [SerializeField]
    private Sprite hectorStandardIconSprite, hectorSelectedIconSprite;

    [Header("Hector skill properties", order = 3)]
    [SerializeField]
    private Image hectorSkillPlaceHolder;

    [SerializeField]
    private Sprite hectorStandardSkillSprite, hectorSelectedSkillSprite;

    #endregion

    #region Hector UI manager

    public void HectorButtonInteractivitySetter()
    {
        hectorButtonRef.interactable = hectorButtonRef.IsInteractable() ? false : true;
    }

    public void SwitchToHectorStateViaButton()
    {
        SetState(new HectorState(this, mainCamera));
    }

    public void HectorSkillButtonController()
    {
        hectorSkillPlaceHolder.enabled = !hectorSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void HectorIconSelectedON()
    {
        hectorIconPlaceHolder.GetComponent<Image>().sprite = hectorSelectedIconSprite;
    }

    public void HectorIconSelectedOFF()
    {
        hectorIconPlaceHolder.GetComponent<Image>().sprite = hectorStandardIconSprite;
    }

    public void HectorOffSkillMode()
    {
        hectorSkillPlaceHolder.GetComponent<Image>().sprite = hectorStandardSkillSprite;
    }

    public void HectorOnSkillMode()
    {
        hectorSkillPlaceHolder.GetComponent<Image>().sprite = hectorSelectedSkillSprite;
    }

    [ContextMenu("Apply damage to Hector - PLAYMODE ONLY!")]
    public void HectorTakingDamage()
    {
        if (hectorCurrentHP > 0)
        {
            hectorCurrentHP--;
            hectorHP.GetComponent<Image>().sprite = hpBars[hectorCurrentHP];
        }
        else
            print("Hector is already dead you sick fuck");
    }

    #endregion
}
