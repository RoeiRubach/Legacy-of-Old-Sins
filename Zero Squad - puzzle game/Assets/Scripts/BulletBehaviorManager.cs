using UnityEngine;

public class BulletBehaviorManager : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private float bulletLifeTime = 7f;

    private void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * bulletSpeed);

        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
