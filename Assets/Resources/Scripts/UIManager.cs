 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public CanvasGroup pause;
    bool isActive = false;
    public Canvas pauseMenu;

    private void Start()
    {
        pause.alpha = 0;
        pauseMenu.GetComponent<Canvas>().enabled = false;
    }
    public void OnStartButtonPress()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnOptionsButtonPress()
    {
        SceneManager.LoadScene("Options");
    }
    public void OnExitButtonPress()
    {
        Application.Quit();
    }

    public void OnMenuButtonPress()
    {
        SceneManager.LoadScene("Start");
    }

    public void OnCreditsButtonPress()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnResumeButtonPress()
    {
        Time.timeScale = 1f;
        pause.alpha = 0;
        isActive = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive == false)
            {
                pauseMenu.GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0f;
                pause.alpha = 1;
                isActive = true;
            }
            else
            {
                pauseMenu.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1f;
                pause.alpha = 0;
                isActive = false;

            }
        }
    }
}
