using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void Start()
    {
        float value;
        if (audioMixer.GetFloat("Volume", out value))
        {
            volumeSlider.value = value;
        }

        // Add listener for slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Set the volume on the mixer
        audioMixer.SetFloat("Volume", volume);
    }
}
