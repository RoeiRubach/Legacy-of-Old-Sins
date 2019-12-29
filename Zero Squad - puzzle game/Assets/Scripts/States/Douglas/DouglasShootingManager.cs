using UnityEngine;

public class DouglasShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    private GameObject bulletClone;

    [SerializeField]
    private Transform bulletHolder;

    private float shootingDelay = 1f;

    private bool isAllowedToShoot = true;

    public void CallSimpleShoot()
    {
        if (isAllowedToShoot)
        {
            bulletClone = Instantiate(bullet, bulletHolder.transform);
            bulletClone.transform.parent = null;

            shootingDelay = 1f;
            isAllowedToShoot = false;
        }
    }

    public void Update()
    {
        if (!isAllowedToShoot)
        {
            shootingDelay -= Time.deltaTime;

            if (shootingDelay <= 0)
            {
                isAllowedToShoot = true;
            }
        }
    }
}
