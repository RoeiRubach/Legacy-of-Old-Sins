using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] enemiesListeners;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Douglas") || other.transform.CompareTag("Hector"))
        {
            foreach (var enemies in enemiesListeners)
            {
                var mp = enemies.GetComponent<MindlessPossessed>();
                if (mp.TargetDetected == null)
                {
                    mp.TargetDetected = other.transform;
                    mp.IsPlayerSpotted = true;
                }
            }
        }
    }
}
