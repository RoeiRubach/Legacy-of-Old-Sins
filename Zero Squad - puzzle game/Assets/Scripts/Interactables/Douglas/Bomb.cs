using UnityEngine;
using UnityEngine.AI;

public class Bomb : InteractableBase, IDouglasInteractables
{
    private void Start()
    {
        //interactedCharacterRef = GameObject.FindGameObjectWithTag(CharactersEnum.Douglas.ToString()).transform;
    }

    private void Update()
    {
        if (isInteract)
        {
            LiftBomb();
            isInteract = false;
        }
    }

    private void LiftBomb()
    {
        FindObjectOfType<PlayerController>().IsLifting = true;
        Destroy(GetComponent<Bomb>());
        Destroy(GetComponent<NavMeshObstacle>());
        Destroy(GetComponent<Outline>());
        transform.parent = objectPlacement;
        transform.localPosition = Vector3.zero;
    }

    public void TriggerBomb()
    {
        Destroy(gameObject, 3f);
    }
}
