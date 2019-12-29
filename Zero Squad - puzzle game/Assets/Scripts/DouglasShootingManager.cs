using UnityEngine;

public class DouglasShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    private GameObject bulletClone;

    [SerializeField]
    private Transform bulletHolder;

    public void CallSimpleShoot()
    {
        bulletClone = Instantiate(bullet, bulletHolder.transform);

        bulletClone.transform.parent = null;
    }
}
