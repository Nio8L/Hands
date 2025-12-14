using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<Clip> clips;

    public AudioSource audioSource;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(string name)
    {
        foreach (Clip clip in clips)
        {
            if (clip.name == name)
            {
                audioSource.PlayOneShot(clip.clip);
            }
        }
    }
}
