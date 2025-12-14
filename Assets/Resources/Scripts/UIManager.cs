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
        Debug.Log(canvasBody == null);
        Debug.Log(canvas == null);
        canvasBody.alpha = 0;
        canvas.GetComponent<Canvas>().enabled = false;
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
        canvasBody.alpha = 1;
        canvas.GetComponent<Canvas>().enabled = true;
    }

    public void OnResumeButtonPress()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
        canvasBody.alpha = 0;
        isActive = false;
    }

    public void OnBackButtonPress()
    {
        canvasBody.alpha = 0;
        canvas.GetComponent<Canvas>().enabled = false;
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
            }
            else
            {
                canvas.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1;
                canvasBody.alpha = 0;
                isActive = false;

            }
        }
    }
}
