using UnityEngine;

public class EnemyVisionRequirement : MonoBehaviour
{
    [SerializeField] private GameObject _enemyEyesPrefab;

    [HideInInspector] public static GameObject _enemyEyesRef;

    private void Awake()
    {
        _enemyEyesRef = Instantiate(_enemyEyesPrefab, transform.parent.position, transform.rotation);
        _enemyEyesRef.transform.parent = gameObject.transform;
    }
}
