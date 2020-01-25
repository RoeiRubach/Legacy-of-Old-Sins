using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _characterToWatch;

    private Vector3 _offset;

    [SerializeField]
    [Range(0,1)]
    [Header("Camera 'jump' to character")]
    private float _lerpingSpeed = 0.1f;

    private void Start()
    {
        _offset = transform.position - _characterToWatch.transform.position;
    }

    private void LateUpdate()
    {
        // Smoothly repositioning the camera's position to the desired character.
        transform.position = Vector3.Lerp(transform.position, _characterToWatch.transform.position + _offset, _lerpingSpeed);
    }

    public void SetCharacter(GameObject newCharacter)
    {
        _characterToWatch = newCharacter;
    }
}
