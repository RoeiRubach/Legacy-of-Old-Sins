using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static Image BackgroundRef { get; private set; }
    public static Sprite DefaultBackground { get; private set; }

    private void Awake()
    {
        BackgroundRef = transform.GetChild(0).GetComponent<Image>();
        DefaultBackground = BackgroundRef.sprite;
    }

    private void OnEnable()
    {
        BackgroundRef.sprite = DefaultBackground;
    }

    public void EnterFirstScene()
    {
        SceneController.LoadScene(_buildIndex: 1);
    }
}