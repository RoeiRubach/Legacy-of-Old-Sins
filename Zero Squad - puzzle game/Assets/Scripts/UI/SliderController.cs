using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Sprite _highlightedSliderHead;
    private Image _imageComponentRef;
    private Sprite _defaultSpriteRef;

    private void Awake()
    {
        _imageComponentRef = GetComponent<Image>();
        _defaultSpriteRef = _imageComponentRef.sprite;
    }

    private void OnMouseOver()
    {
        _imageComponentRef.sprite = _highlightedSliderHead;
    }

    private void OnMouseExit()
    {
        _imageComponentRef.sprite = _defaultSpriteRef;
    }
}
