using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PlayAgian : MonoBehaviour
{
 public void OnPlayAgainButtonPress()
    {
        SceneManager.LoadScene("Start");
    }
}
