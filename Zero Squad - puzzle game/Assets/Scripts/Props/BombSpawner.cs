using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bombRef;

    public void SpawnABomb()
    {
        var bomb = Instantiate(_bombRef, transform.position, Quaternion.identity);
        bomb.transform.parent = transform;
    }
}
