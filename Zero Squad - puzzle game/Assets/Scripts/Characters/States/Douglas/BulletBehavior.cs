using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private string _hectorShield = "Hector Shield", _enemyTag = "Enemy";
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
        if (other.gameObject.CompareTag(_enemyTag))
            Destroy(gameObject);

        else if (!other.gameObject.CompareTag(_hectorShield))
            Destroy(gameObject);
    }
}
