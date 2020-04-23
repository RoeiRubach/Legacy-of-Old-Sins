using UnityEngine;

public class TriggerBomb : MonoBehaviour
{
    [Range(1, 5)]
    [SerializeField] private int _bombTimer;

    public void TriggerBombInSeconds()
    {
        Destroy(gameObject, _bombTimer);
    }
}
