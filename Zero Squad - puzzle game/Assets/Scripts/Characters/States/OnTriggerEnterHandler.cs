using UnityEngine;

public class OnTriggerEnterHandler : MonoBehaviour, IPuzzleAuthority
{
    private PlayerController playerController;

    private PlayerStateManager _characterTrigger;

    private void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

        if (transform.name.StartsWith(CharactersEnum.Douglas.ToString()))
            _characterTrigger = new DouglasState(playerController);

        else if (transform.name.StartsWith(CharactersEnum.Elena.ToString()))
            _characterTrigger = new ElenaState(playerController);

        else if (transform.name.StartsWith(CharactersEnum.Hector.ToString()))
            _characterTrigger = new HectorState(playerController);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            _characterTrigger.OnTriggerEnter(other.tag);
    }
}
