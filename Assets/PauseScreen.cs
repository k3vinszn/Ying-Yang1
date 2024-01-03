using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu; // Assign your pause menu GameObject in the Unity Inspector
    public Button resumeButton; // Assign your resume button in the Unity Inspector

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the resume button is set up to call the ResumeButtonClick method
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeButtonClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Esc key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void OnPauseButtonClick()
    {
        TogglePause();
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true); // Enable the pause menu GameObject
                pauseButton.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false); // Disable the pause menu GameObject
                pauseButton.SetActive(true);
            }
        }
    }

    void ResumeButtonClick()
    {
        TogglePause();
    }
}
