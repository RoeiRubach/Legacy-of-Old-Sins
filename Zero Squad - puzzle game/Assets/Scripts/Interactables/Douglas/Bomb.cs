using UnityEngine;
using UnityEngine.AI;

public class Bomb : InteractableBase, IDouglasInteractables
{
    private void Update()
    {
        if (!isInteract) return;

        LiftBomb();
        isInteract = false;
    }

    private void LiftBomb()
    {
        FindObjectOfType<PlayerController>().IsLifting = true;
        Destroy(GetComponent<Bomb>());
        Destroy(GetComponent<NavMeshObstacle>());
        Destroy(GetComponent<Outline>());
        if (objectPlacement == null)
            SetObjectPlacement();
        transform.parent = objectPlacement;
        transform.localPosition = Vector3.zero;
    }

    private void SetObjectPlacement()
    {
        objectPlacement = GameObject.Find("Bomb carry placement").transform;
    }
}
