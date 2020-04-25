using UnityEngine;

public class OnTriggerEnterHandler : MonoBehaviour
{
    private PlayerController playerController;

    private PlayerStateManager _characterTrigger;

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

        if (transform.name.StartsWith("Douglas"))
            _characterTrigger = new DouglasState(playerController);

        else if (transform.name.StartsWith("Elena"))
            _characterTrigger = new ElenaState(playerController);

        else if (transform.name.StartsWith("Hector"))
            _characterTrigger = new HectorState(playerController);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("HealthRegen"))
            _characterTrigger.OnTriggerEnter(other.tag, other.GetComponent<HealthRegenCollectables>());
    }
}
