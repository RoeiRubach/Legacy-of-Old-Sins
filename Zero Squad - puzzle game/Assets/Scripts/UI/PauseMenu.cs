using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;

    [SerializeField] private GameObject _onGameUI;
    [SerializeField] private GameObject _pauseMenuUI;

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
        if (TutorialPopUpsController.Instance.IsShowingPopup)
            TutorialPopUpsController.Instance.DisplayFirstChild();
        _pauseMenuUI?.SetActive(false);
        _onGameUI?.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void PauseGame()
    {
        TutorialPopUpsController.Instance.SavePopUpForPauseMenu();
        _onGameUI?.SetActive(false);
        _pauseMenuUI?.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
