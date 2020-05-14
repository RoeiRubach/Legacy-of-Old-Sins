using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] enemiesListeners;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Douglas") || other.transform.CompareTag("Hector"))
        {
            //if (!other.transform.GetComponent<CharactersPoolController>().isActiveAndEnabled)
            //    other.transform.GetComponent<CharactersPoolController>().enabled = true;

            if (enemiesListeners[0] != null)
            {
                foreach (var enemies in enemiesListeners)
                {
                    if (enemies.gameObject.activeInHierarchy)
                        enemies.GetComponent<MindlessPossessed>().IsPlayerSpotted = true;
                }
            }
            Destroy(gameObject);
        }
    }
}
