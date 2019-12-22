using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject characterToWatch;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - characterToWatch.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, characterToWatch.transform.position + offset, 0.1f);
    }

    public void SetCharacter(GameObject newCharacter)
    {
        characterToWatch = newCharacter;
    }
}
