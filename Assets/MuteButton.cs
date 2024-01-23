using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    // Reference to the button UI element
    public Button muteButton;

    // Reference to the Image component for the button
    public Image buttonImage;

    // Images for the button in mute and unmute states
    public Sprite muteSprite;
    public Sprite unmuteSprite;

    // Key to store mute state in PlayerPrefs
    private const string MuteKey = "IsMuted";
    private const string OriginalVolumeKey = "OriginalVolume";

    private void Start()
    {
        // Load mute state from PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;

        // Set the initial button text and image based on the mute state
        UpdateButtonVisuals(isMuted);

        // Subscribe to the button click event
        muteButton.onClick.AddListener(OnMuteButtonClick);
    }

    private void OnMuteButtonClick()
    {
        // Toggle mute state
        bool isMuted = !IsMuted();

        if (isMuted)
        {
            // Store the current volume before muting
            float currentVolume = AudioListener.volume;
            PlayerPrefs.SetFloat(OriginalVolumeKey, currentVolume);
            PlayerPrefs.Save();
        }

        // Set the mute state in PlayerPrefs
        PlayerPrefs.SetInt(MuteKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        // Set the button text and image based on the new mute state
        UpdateButtonVisuals(isMuted);

        // Mute or unmute the game sounds
        SetMuteState(isMuted);
    }

    private bool IsMuted()
    {
        // Get the mute state from PlayerPrefs
        return PlayerPrefs.GetInt(MuteKey, 0) == 1;
    }

    private void UpdateButtonVisuals(bool isMuted)
    {
        // Set the button text based on the mute state
        muteButton.GetComponentInChildren<Text>().text = isMuted ? "Unmute" : "Mute";

        // Change the button image based on the mute state
        buttonImage.sprite = isMuted ? muteSprite : unmuteSprite;
    }

    private void SetMuteState(bool isMuted)
    {
        if (isMuted)
        {
            // Mute the game sounds
            AudioListener.volume = 0f;
        }
        else
        {
            // Unmute the game sounds and set the volume to the original volume
            float originalVolume = PlayerPrefs.GetFloat(OriginalVolumeKey, 1f);
            AudioListener.volume = originalVolume;
        }
    }
}
