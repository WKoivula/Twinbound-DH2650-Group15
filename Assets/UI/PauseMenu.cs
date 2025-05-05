using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        bool isActive = pauseMenuUI.activeSelf;
        pauseMenuUI.SetActive(!isActive);

        Time.timeScale = isActive ? 1f : 0f; // Pause the game when menu is active
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Resume before loading
        SceneManager.LoadScene("MainMenu"); // Make sure scene name matches
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
