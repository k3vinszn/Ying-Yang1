using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Reference to the Slider UI element
    public Slider volumeSlider;

    // Key to store volume in PlayerPrefs
    private const string VolumeKey = "Volume";

    private void Start()
    {
        // Load volume from PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        SetVolume(savedVolume);

        // Set the slider value to the loaded volume
        volumeSlider.value = savedVolume;

        // Subscribe to the slider value changed event
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        // Set the volume when the slider value changes
        SetVolume(volume);

        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    private void SetVolume(float volume)
    {
        // Set the volume in your game or application
        // For example, adjust the AudioListener volume
        AudioListener.volume = volume;
    }
}
