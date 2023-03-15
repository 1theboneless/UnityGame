using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused = false;
    void Start()
    {
        
        ResumeGame();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    // Call this function to toggle the pause menu on/off
    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Pause the game and activate the pause menu UI
    void PauseGame()
    {
        Time.timeScale = 0f; // Stop time
        isPaused = true;
        pauseMenuUI.SetActive(true);
    }

    // Resume the game and deactivate the pause menu UI
    void ResumeGame()
    {
        Time.timeScale = 1f; // Start time
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    // Call this function to return to the main menu
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Start time
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}