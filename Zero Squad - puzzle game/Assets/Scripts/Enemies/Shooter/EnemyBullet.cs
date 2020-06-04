using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Range(1f, 5f)]
    [SerializeField] private int _bulletDamage = 1;

    private Vector3 _shootDirection;
    private float _moveSpeed = 11f;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void SetUp(Vector3 shootDirection)
    {
        _shootDirection = shootDirection;
        transform.eulerAngles = new Vector3(90, GetAngleFromVectorFloat(_shootDirection), 0);
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += _shootDirection * Time.deltaTime * _moveSpeed;

        var nearestEnemy = CharactersPoolController.FindClosestEnemy(transform.position);
        float distance = Vector3.Distance(transform.position, nearestEnemy.transform.position);

        if(distance <= 1.65f)
        {
            _playerController.DamageACharacter(nearestEnemy.transform, _bulletDamage);
            Destroy(gameObject);
        }
    }

    private float GetAngleFromVectorFloat(Vector3 direction)
    {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
    }
}
