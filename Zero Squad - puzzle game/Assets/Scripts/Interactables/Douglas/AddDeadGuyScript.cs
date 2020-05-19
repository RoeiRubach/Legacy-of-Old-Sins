using UnityEngine;

public class AddDeadGuyScript : MonoBehaviour
{
    public void AddDeadGuyScriptToGameObjectViaEvent()
    {
        gameObject.AddComponent<DeadGuyWithAKey>();
        Destroy(GetComponent<AddDeadGuyScript>());
    }
}
