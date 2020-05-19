using UnityEngine;

public class AddConsoleScript : MonoBehaviour
{
    public void AddConsoleScriptToGameObjectViaEvent()
    {
        gameObject.AddComponent<Console>();
        Destroy(GetComponent<AddConsoleScript>());
    }
}
