using UnityEngine;

public class Bomb : InteractableBase, IDouglasInteractables
{
    [SerializeField] private GameObject _bombCarryPrefab;

    private void Start()
    {
        transform.name = "Bomb";
    }

    private void Update()
    {
        if (!isInteract) return;

        LiftBomb();
        isInteract = false;
    }

    private void LiftBomb()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.IsLifting = true;
        if (objectPlacement == null)
            SetObjectPlacement();
        GetComponentInChildren<Animator>().SetBool("DouglasLifingBomb", true);
        playerController.BombRef = Instantiate(_bombCarryPrefab, objectPlacement);
        //transform.parent = objectPlacement;
        //transform.localPosition = Vector3.zero;
        Destroy(GetComponent<Bomb>());
        Destroy(GetComponent<Outline>());
    }

    private void SetObjectPlacement()
    {
        objectPlacement = GameObject.Find("Bomb carry placement").transform;
    }
}
