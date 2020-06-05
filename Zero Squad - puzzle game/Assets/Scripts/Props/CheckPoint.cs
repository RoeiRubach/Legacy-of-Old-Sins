using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Douglas") || (other.transform.CompareTag("Elena") || (other.transform.CompareTag("Hector"))))
        {
            GameManager.Instance.IsReachedCheckPoint = true;
            for (int i = 0; i < GameManager.Instance.CharactersPlacements.Length; i++)
            {
                GameManager.Instance.CharactersPlacements[i] = transform.GetChild(i).position;
            }
            GetComponent<GameEventSubscriber>()?.OnEventFire();
            Destroy(gameObject);
        }
    }
}
