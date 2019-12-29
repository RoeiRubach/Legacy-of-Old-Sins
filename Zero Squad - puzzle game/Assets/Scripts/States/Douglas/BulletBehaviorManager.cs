using UnityEngine;

public class BulletBehaviorManager : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private float bulletLifeTime = 7f;

    private void FixedUpdate()
    {
        transform.position += transform.forward * (Time.deltaTime * bulletSpeed);

        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Hector Shield"))
        {
            Destroy(gameObject);
        }
    }
}
