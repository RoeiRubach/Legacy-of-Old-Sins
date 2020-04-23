using UnityEngine;

public enum CharactersEnum
{
    Douglas,
    Elena,
    Hector
}

public class InteractableBase : MonoBehaviour
{
    protected bool isInteract;

    protected Transform interactedCharacterRef;

    [SerializeField] protected Transform characterInteractionPlacement;
    [SerializeField] protected Transform objectPlacement;

    public Vector3 CharacterInteractionPlacement()
    {
        if (characterInteractionPlacement != null)
            return characterInteractionPlacement.position;
        else
            return interactedCharacterRef.localPosition;
    }


    public virtual void Interact()
    {
        if (!isInteract)
        {
            Debug.Log("interacted");
            isInteract = true;
        }
    }
}
