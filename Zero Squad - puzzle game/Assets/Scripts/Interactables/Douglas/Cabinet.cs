using UnityEngine;

public class Cabinet : InteractableBase, IDouglasInteractables
{
    private void Update()
    {
        if (isInteract)
        {
            transform.position = Vector3.Lerp(transform.position, objectPlacement.position, 0.01f);

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
