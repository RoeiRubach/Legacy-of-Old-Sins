using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private List<MindlessPossessed> enemiesListeners;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Douglas") || other.transform.CompareTag("Hector"))
        {
            //if (!other.transform.GetComponent<CharactersPoolController>().isActiveAndEnabled)
            //    other.transform.GetComponent<CharactersPoolController>().enabled = true;

            if (enemiesListeners != null)
            {
                foreach (var enemies in enemiesListeners)
                {
                    if(enemies != null)
                        if (enemies.gameObject.activeSelf)
                            enemies.IsPlayerSpotted = true;
                }
            }
            Destroy(gameObject);
        }
    }
}
