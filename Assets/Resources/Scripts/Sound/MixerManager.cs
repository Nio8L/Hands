using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    public AudioMixer mixer;

    private void Start() {
        SetMasterVolume(0.5f);
    }

    public void SetMasterVolume(float level)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }
}
