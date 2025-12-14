using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    public static LevelTracker instance;
    public int level = 1;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NextLevel()
    {
        level++;
        SceneManager.LoadScene("Level" + level);
    }

    private void Reset(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            level = 1;
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += Reset;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= Reset;   
    }

}
