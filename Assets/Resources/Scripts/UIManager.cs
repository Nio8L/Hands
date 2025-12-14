 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public CanvasGroup canvasBody;
    bool isActive = false;
    public Canvas canvas;

    private void Start()
    {
        canvasBody.alpha = 0;
        canvas.GetComponent<Canvas>().enabled = false;
    }
    public void OnStartButtonPress()
    {
        SceneManager.LoadScene("Level1");
        SoundManager.instance.Play("Click");
    }

    public void OnOptionsButtonPress()
    {
        OptionsMenu.instance.Open();
        SoundManager.instance.Play("Click");
    }

    
    public void OnExitButtonPress()
    {
        SoundManager.instance.Play("Click");
        Application.Quit();
    }

    public void OnMenuButtonPress()
    {
        SceneManager.LoadScene("Start");
        SoundManager.instance.Play("Click");
    }

    public void OnCreditsButtonPress()
    {
        canvasBody.alpha = 1;
        canvas.GetComponent<Canvas>().enabled = true;
        SoundManager.instance.Play("Click");
    }

    public void OnResumeButtonPress()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
        canvasBody.alpha = 0;
        isActive = false;
        SoundManager.instance.Play("Click");
    }

    public void OnBackButtonPress()
    {
        canvasBody.alpha = 0;
        canvas.GetComponent<Canvas>().enabled = false;
        SoundManager.instance.Play("Click");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive == false)
            {
                canvas.GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0;
                canvasBody.alpha = 1;
                isActive = true;
                SoundManager.instance.Play("Click");
            }
            else
            {
                canvas.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1;
                canvasBody.alpha = 0;
                isActive = false;
                SoundManager.instance.Play("Click");
            }
        }
    }
}
