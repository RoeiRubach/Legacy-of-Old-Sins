using UnityEngine;

public enum CharactersEnum
{
    Douglas,
    Elena,
    Hector
}

[RequireComponent(typeof(Outline))]
public class InteractableBase : MonoBehaviour, IInteractable
{
    protected bool isInteract;

    //protected Transform interactedCharacterRef;

    [SerializeField] protected Transform characterInteractionPlacement;
    [SerializeField] protected Transform objectPlacement;

    public Vector3 CharacterInteractionPlacement()
    {
        if (characterInteractionPlacement != null)
            return characterInteractionPlacement.position;
        else
            return transform.localPosition;
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
