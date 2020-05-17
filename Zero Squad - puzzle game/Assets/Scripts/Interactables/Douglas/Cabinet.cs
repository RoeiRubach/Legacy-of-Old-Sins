using UnityEngine;

public class Cabinet : InteractableBase, IDouglasInteractables
{
    [Range(0.01f, 0.018f)]
    [SerializeField] private float _transitionCabinet = 0.018f;

    private void Update()
    {
        if (isInteract)
        {
            transform.position = Vector3.Lerp(transform.position, objectPlacement.position, _transitionCabinet);

            float disctance = Vector3.Distance(objectPlacement.position, transform.position);

            if (disctance <= 0.035f)
            {
                GetComponent<GameEventSubscriber>()?.OnEventFire();
                isInteract = false;
                Destroy(GetComponent<Cabinet>());
                Destroy(GetComponent<Outline>());
            }
        }
    }
}
