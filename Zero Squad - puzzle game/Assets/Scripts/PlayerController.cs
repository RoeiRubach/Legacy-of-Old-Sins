using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private const int maxHP = 10;

    private PlayerStateManager currentState;

    [SerializeField]
    private CameraController mainCamera;
    
    public LayerMask walkableLayerMask;

    [SerializeField]
    private Sprite[] hpBars;

    #region Douglas attributes
    [Space(height: 20)]

    [Header("Douglas attributes", order = 1)]
    [SerializeField]
    private Image douglasHP;

    private int douglasCurrentHP = maxHP; 

    [Header("Douglas icon properties", order = 2)]
    [SerializeField]
    private Image douglasIconPlaceHolder;

    [SerializeField]
    private Sprite douglasStandardIconSprite, douglasSelectedIconSprite;

    [Header("Douglas skill properties", order = 3)]
    [SerializeField]
    private Image douglasSkillPlaceHolder;

    [SerializeField]
    private Sprite douglasStandardSkillSprite, douglasSelectedSkillSprite;

    #endregion

    #region Elena attributes
    [Space(height:20)]

    [Header("Elena attributes", order = 1)]
    [SerializeField]
    private Image elenaHP;

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

    #region Hector attributes
    [Space(height: 20)]

    [Header("Hector attributes", order = 1)]
    [SerializeField]
    private Image hectorHP;

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

    private void Start()
    {
        SetState(new DouglasState(this, mainCamera));
    }

    private void Update()
    {
        currentState.UpdateHandle();
    }

    public void SetState(PlayerStateManager playerState)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = playerState;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    #region Douglas UI manager

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

    public void douglasSkillButtonController()
    {
        douglasSkillPlaceHolder.enabled = !douglasSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void douglasOffSkillMode()
    {
        douglasSkillPlaceHolder.GetComponent<Image>().sprite = douglasStandardSkillSprite;
    }

    public void douglasOnSkillMode()
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

    #region Elena UI manager

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

    public void elenaSkillButtonController()
    {
        elenaSkillPlaceHolder.enabled = !elenaSkillPlaceHolder.isActiveAndEnabled ? true : false;
    }

    public void elenaOffSkillMode()
    {
        elenaSkillPlaceHolder.GetComponent<Image>().sprite = elenaStandardSkillSprite;
    }

    public void elenaOnSkillMode()
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

    #region Hector UI manager

    public void SwitchToHectorStateViaButton()
    {
        SetState(new HectorState(this, mainCamera));
    }

    public void hectorSkillButtonController()
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

    public void hectorOffSkillMode()
    {
        hectorSkillPlaceHolder.GetComponent<Image>().sprite = hectorStandardSkillSprite;
    }

    public void hectorOnSkillMode()
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

    public void EnterSkillViaButton()
    {
        currentState.enterSkillViaButton = !currentState.enterSkillViaButton ? true : false;
    }
}
