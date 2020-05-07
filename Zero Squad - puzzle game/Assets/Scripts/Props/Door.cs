using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isInvoke;

    private void FixedUpdate()
    {
        if (isInvoke)
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
    }

    public void Invoke()
    {
        isInvoke = true;
        Destroy(gameObject, 3);
    }
}
