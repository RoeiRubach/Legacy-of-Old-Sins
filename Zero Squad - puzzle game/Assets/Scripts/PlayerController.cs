using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerStateManager currentState;

    [SerializeField]
    private CameraController mainCamera;
    
    public LayerMask walkableLayerMask;

    #region Douglas attributes
    [Space(height: 20)]

    [Header("Douglas attributes", order = 1)]
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
    #endregion

    public void EnterSkillViaButton()
    {
        currentState.enterSkillViaButton = !currentState.enterSkillViaButton ? true : false;
    }
}
