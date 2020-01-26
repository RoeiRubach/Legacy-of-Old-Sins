using UnityEngine;

[ExecuteInEditMode]
public class EnemyVisionRequirement : MonoBehaviour
{
    [TextArea] [SerializeField] private string _enemyVisionInstructions = "There's an 'Enemy vision' game object as a child. Change its transform as you like. If a player walks in it, the enemy will detect the player.";

    [SerializeField] private GameObject _enemyEyesPrefab;

    private Transform[] _allGameObjectChildren;

    private GameObject _enemyEyesRef;

    private bool _isEnemyVisionFound;

    private void Start()
    {
        _allGameObjectChildren = GetComponentsInChildren<Transform>();

        foreach (var transforms in _allGameObjectChildren)
        {
            if (transforms.CompareTag("Enemy Vision"))
            {
                _isEnemyVisionFound = true;
                break;
            }
        }

        if (!_isEnemyVisionFound)
        {
            _enemyEyesRef = Instantiate(_enemyEyesPrefab, transform.position, transform.rotation);
            _enemyEyesRef.transform.parent = gameObject.transform;
        }
    }

}
