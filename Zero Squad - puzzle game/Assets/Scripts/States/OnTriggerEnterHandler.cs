using UnityEngine;

public class OnTriggerEnterHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private PlayerStateManager _characterTrigger;

    private void Start()
    {
        if (transform.name.StartsWith("Douglas"))
            _characterTrigger = new DouglasState(playerController);

        else if (transform.name.StartsWith("Elena"))
            _characterTrigger = new ElenaState(playerController);

        else if (transform.name.StartsWith("Hector"))
            _characterTrigger = new HectorState(playerController);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _characterTrigger.OnTriggerEnter();
        }
    }
}
