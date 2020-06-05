using UnityEngine;

public class CheckPointText : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("InvokedSetActiveFalse", 3f);
    }

    private void InvokedSetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
