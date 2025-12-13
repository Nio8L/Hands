 using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public void OnStartButtonPress()
    {
        SceneManager.LoadScene("Level1");
    }
}
