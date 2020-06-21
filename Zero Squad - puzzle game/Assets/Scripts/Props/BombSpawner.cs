using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bombRef;

    public void SpawnABomb()
    {
        if(transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        var bomb = Instantiate(_bombRef, transform.position, Quaternion.identity);
        bomb.transform.parent = transform;
    }
}
