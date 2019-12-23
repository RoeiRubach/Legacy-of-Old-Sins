using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject characterToWatch;

    private Vector3 offset;

    [SerializeField]
    [Range(0,1)]
    [Header("Camera 'jump' to character")]
    private float lerpingSpeed = 0.1f;

    private void Start()
    {
        offset = transform.position - characterToWatch.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, characterToWatch.transform.position + offset, lerpingSpeed);
    }

    public void SetCharacter(GameObject newCharacter)
    {
        characterToWatch = newCharacter;
    }
}
