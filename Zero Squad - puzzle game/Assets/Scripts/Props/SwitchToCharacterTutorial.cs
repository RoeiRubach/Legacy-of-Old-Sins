using UnityEngine;

public class SwitchToCharacterTutorial : MonoBehaviour
{
    [SerializeField] private bool _isHector;
    [SerializeField] private Door _door;
    [SerializeField] private GameObject _enemies;

    private void Update()
    {
        if (_isHector)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
                ContinueOnTutorial();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
                ContinueOnTutorial();
        }
    }

    public void ContinueOnTutorial()
    {
        _door.Invoke();
        _enemies.SetActive(true);
        Destroy(this);
    }
}
