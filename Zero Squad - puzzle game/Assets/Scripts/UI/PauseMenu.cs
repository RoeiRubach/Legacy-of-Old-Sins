using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;

    [SerializeField] private GameObject _onGameUI;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _popUpsRef;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        _pauseMenuUI?.SetActive(false);
        _onGameUI?.SetActive(true);
        _popUpsRef?.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void PauseGame()
    {
        _onGameUI?.SetActive(false);
        _popUpsRef?.SetActive(false);
        _pauseMenuUI?.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
