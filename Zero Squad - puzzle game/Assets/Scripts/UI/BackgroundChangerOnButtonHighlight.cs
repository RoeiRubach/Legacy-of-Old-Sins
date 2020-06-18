using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundChangerOnButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite _backgroundToChangeTo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MainMenu.BackgroundRef.sprite = _backgroundToChangeTo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MainMenu.BackgroundRef.sprite = MainMenu.DefaultBackground;
    }
}
