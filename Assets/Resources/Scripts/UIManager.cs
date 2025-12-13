 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class UIManager : MonoBehaviour
{
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

    public void OnCreditsButtonPress()
    {
        SceneManager.LoadScene("Credits");
    }

    public void pauseMenu()
    {
        if (Input.GetKeyDown(Key.Escape))
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
    }
}
