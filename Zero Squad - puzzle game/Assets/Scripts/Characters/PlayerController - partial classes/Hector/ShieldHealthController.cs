using UnityEngine;

public class ShieldHealthController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
            GetComponent<EnemyHealth>().HealthDecreaseViaBullet();
    }
}
