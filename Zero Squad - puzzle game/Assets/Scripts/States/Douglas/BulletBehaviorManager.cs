using UnityEngine;

public class BulletBehaviorManager : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private float _bulletLifeTime = 7f;

    private void FixedUpdate()
    {
        transform.position += transform.forward * (Time.deltaTime * _bulletSpeed);

        _bulletLifeTime -= Time.deltaTime;
        if (_bulletLifeTime <= 0)
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
