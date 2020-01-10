using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController
{
    #region Douglas attributes

    [Header("Douglas attributes", order = 0)]
    [SerializeField]
    private Image douglasHP;
    [SerializeField]
    private Button douglasButtonRef;

    private int douglasCurrentHP = maxHP;

    [Header("Douglas icon properties", order = 1)]
    [SerializeField]
    private Image douglasIconPlaceHolder;

    [SerializeField]
    private Sprite douglasStandardIconSprite, douglasSelectedIconSprite;

    [Header("Douglas skill properties", order = 2)]
    [SerializeField]
    private Image douglasSkillPlaceHolder;

    [SerializeField]
    private Sprite douglasStandardSkillSprite, douglasSelectedSkillSprite;

    #endregion

    #region Douglas UI manager

    public void DouglasButtonInteractivitySetter()
    {
        douglasButtonRef.interactable = douglasButtonRef.IsInteractable() ? false : true;
    }

    public void SwitchToDouglasStateViaButton()
    {
        SetState(new DouglasState(this, mainCamera));
    }

    public void DouglasIconSelectedON()
    {
        douglasIconPlaceHolder.GetComponent<Image>().sprite = douglasSelectedIconSprite;
    }

    public void DouglasIconSelectedOFF()
    {
        douglasIconPlaceHolder.GetComponent<Image>().sprite = douglasStandardIconSprite;
    }

    public void DouglasSkillButtonController()
    {
        douglasSkillPlaceHolder.enabled = !douglasSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void DouglasOffSkillMode()
    {
        douglasSkillPlaceHolder.GetComponent<Image>().sprite = douglasStandardSkillSprite;
    }

    public void DouglasOnSkillMode()
    {
        douglasSkillPlaceHolder.GetComponent<Image>().sprite = douglasSelectedSkillSprite;
    }

    [ContextMenu("Apply damage to Douglas - PLAYMODE ONLY!")]
    public void DouglasTakingDamage()
    {
        if (douglasCurrentHP > 0)
        {
            douglasCurrentHP--;
            douglasHP.GetComponent<Image>().sprite = hpBars[douglasCurrentHP];
        }
        else
            print("Douglas is already dead you sick fuck");
    }

    #endregion
}
