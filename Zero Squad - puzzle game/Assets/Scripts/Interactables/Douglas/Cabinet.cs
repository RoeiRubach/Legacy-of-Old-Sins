using UnityEngine;

public class Cabinet : InteractableBase, IDouglasInteractables
{
    private void Start()
    {
        interactedCharacterRef = GameObject.FindGameObjectWithTag(CharactersEnum.Douglas.ToString()).transform;
    }

    private void Update()
    {
        if (isInteract)
        {
            transform.position = Vector3.Lerp(transform.position, objectPlacement.position, 0.01f);

            float disctance = Vector3.Distance(objectPlacement.position, transform.position);

            if (disctance <= 0.05f)
            {
                isInteract = false;
                Destroy(GetComponent<Cabinet>());
            }
        }
    }
}
