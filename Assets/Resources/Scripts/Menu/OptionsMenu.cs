using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu instance;

    public GameObject options;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Open()
    {
        options.SetActive(true);
    }

    public void Close()
    {
        options.SetActive(false);
        SoundManager.instance.Play("Click");
    }
}
